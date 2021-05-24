using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Toyota.Common.Credential;
using System.Security.Policy;
using Toyota.Common.Web.Platform;
using System.Web.SessionState;
using Toyota.Common;
using Toyota.Common.Utilities;
using Toyota.Common.Lookup;

namespace GPPSU.Commons.Menus
{
    public class MockingMenu
    {
        public MenuList MENU
        {
            get
            {
                MenuList m = new MenuList();
                IMenuProvider provider = ProviderRegistry.Instance.Get<IMenuProvider>();
                provider.Load();
                m = provider.GetAll();
                return m;
            }
        }

        public Toyota.Common.Credential.User USER
        {
            get
            {
                ILookup l = HttpContext.Current.Session["__tdk_lookup__"] as ILookup;
                if (l.IsNull()) return null;
                return l.Get<Toyota.Common.Credential.User>();
            }
        }

        public MenuList AuthorizedMenu
        {
            get
            {
                MenuList menu = MENU;
                if (menu == null)
                {
                    return null;
                }

                User user = USER;
                if (user == null)
                {
                    return new MenuList();
                }

                string UserMenuKey = "tdk.AuthorizedMenu." + user.Username;

                HttpContext httpContext = HttpContext.Current;
                HttpSessionState session = httpContext.Session;
                if (!session.IsNewSession)
                {
                    object o_auth_menu = session[UserMenuKey];
                    if (!o_auth_menu.IsNull())
                    {
                        return (MenuList)o_auth_menu;
                    }
                }

                MenuList authorizedMenu = new MenuList();
                menu.IterateByAction(m =>
                {
                    authorizedMenu.Add(m.Clone());
                });

                IList<Menu> unauthorizedMenu = new List<Menu>();
                authorizedMenu.IterateByAction(m =>
                {
                    if (!IsMenuAuthorized(m, user))
                    {
                        unauthorizedMenu.Add(m);
                    }
                });

                unauthorizedMenu.IterateByAction(m =>
                {
                    authorizedMenu.Remove(m);
                });

                session["UserSessionMenuKey"] = authorizedMenu;

                return authorizedMenu;
            }
        }

        private bool IsMenuAuthorized(Menu menu, User user)
        {
            bool authorized = IsMenuItemAuthorized(menu, user);
            if (authorized && menu.HasChildren())
            {
                IList<Menu> unauthorizedChildren = new List<Menu>();
                menu.Children.IterateByAction(m =>
                {
                    if (!IsMenuAuthorized(m, user))
                    {
                        unauthorizedChildren.Add(m);
                    }
                });
                unauthorizedChildren.IterateByAction(m =>
                {
                    menu.RemoveChildren(m);
                });
            }
            return authorized;
        }
        private bool IsMenuItemAuthorized(Menu menu, User user)
        {
            if (menu.IsNull())
            {
                return false;
            }
            if (menu.Roles.IsNullOrEmpty())
            {
                return true;
            }
            if (user.IsNull())
            {
                return false;
            }
            if (user.Roles.IsNullOrEmpty())
            {
                return false;
            }

            bool authorized = false;
            string roleId;
            AuthorizationFunction _mfunction;
            AuthorizationFunction _function;
            AuthorizationFeature _mfeature;
            AuthorizationFeature _feature;
            AuthorizationFeatureQualifier _mqualifier;
            AuthorizationFeatureQualifier _qualifier;

            foreach (Role role in user.Roles)
            {
                roleId = role.Id;
                foreach (Role mrole in menu.Roles)
                {
                    _mfunction = null;
                    _function = null;
                    _mfeature = null;
                    _feature = null;
                    _mqualifier = null;
                    _qualifier = null;

                    if (!mrole.Id.Equals(roleId))
                    {
                        authorized |= false;
                        continue;
                    }

                    if (mrole.Functions.IsNullOrEmpty())
                    {
                        authorized |= true;
                        continue;
                    }
                    if (role.Functions.IsNullOrEmpty())
                    {
                        authorized |= false;
                        continue;
                    }

                    mrole.Functions.FindAgainst(role.Functions, (tfunc, ofunc) =>
                    {
                        return tfunc.Id.StringEquals(ofunc.Id);
                    }, (tfunc, ofunc) =>
                    {
                        _mfunction = tfunc;
                        _function = ofunc;
                    });
                    if (_mfunction.IsNull())
                    {
                        authorized |= false;
                        continue;
                    }

                    authorized |= true;

                    if (_mfunction.Features.IsNullOrEmpty())
                    {
                        continue;
                    }
                    if (_function.Features.IsNullOrEmpty())
                    {
                        authorized = false;
                        continue;
                    }

                    foreach (AuthorizationFunction func in role.Functions)
                    {
                        _mfunction.Features.FindAgainst(func.Features, (tfeat, ofeat) =>
                        {
                            return tfeat.Id.StringEquals(ofeat.Id);
                        }, (tfeat, ofeat) =>
                        {
                            _mfeature = tfeat;
                            _feature = ofeat;
                        });
                    }
                    if (_mfeature.IsNull())
                    {
                        authorized = false;
                        continue;
                    }
                    if (_mfeature.Qualifiers.IsNullOrEmpty())
                    {
                        continue;
                    }
                    if (_feature.Qualifiers.IsNullOrEmpty())
                    {
                        authorized = false;
                        continue;
                    }

                    _mfeature.Qualifiers.FindAgainst(_feature.Qualifiers, (tq, oq) =>
                    {
                        return tq.Key.StringEquals(oq.Key) && tq.Qualifier.StringEquals(oq.Qualifier);
                    }, (tq, oq) =>
                    {
                        _mqualifier = tq;
                        _qualifier = oq;
                    });
                    if (_mqualifier == null)
                    {
                        authorized = false;
                        continue;
                    }
                    if (!_mqualifier.Qualifier.Equals(_qualifier.Qualifier))
                    {
                        authorized = false;
                    }
                }
            }

            return authorized;
        }

        public static string Mocking(MenuList x, int level = 0)
        {
            StringBuilder b = new StringBuilder("");
            string pre = new string(' ', (level + 1) * 2);

            if (x != null)
                foreach (Menu m in x)
                {
                    if (m == null)
                        continue;
                    string roles = string.Join(";", m.Roles.Select(r => r.Id).ToList());
                    b.AppendFormat("#{0,-42} {1,-40} {2,-40} [{3}]\r\n", pre + m.Id, m.NavigateUrl, m.Text, roles);
                    if (m.HasChildren())
                        b.Append(Mocking(m.Children, level + 1));
                }

            return b.ToString();
        }
    }
}