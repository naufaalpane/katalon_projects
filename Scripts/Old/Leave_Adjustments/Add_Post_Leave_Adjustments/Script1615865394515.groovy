import static com.kms.katalon.core.checkpoint.CheckpointFactory.findCheckpoint
import static com.kms.katalon.core.testcase.TestCaseFactory.findTestCase
import static com.kms.katalon.core.testdata.TestDataFactory.findTestData
import static com.kms.katalon.core.testobject.ObjectRepository.findTestObject
import static com.kms.katalon.core.testobject.ObjectRepository.findWindowsObject
import com.kms.katalon.core.checkpoint.Checkpoint as Checkpoint
import com.kms.katalon.core.cucumber.keyword.CucumberBuiltinKeywords as CucumberKW
import com.kms.katalon.core.mobile.keyword.MobileBuiltInKeywords as Mobile
import com.kms.katalon.core.model.FailureHandling as FailureHandling
import com.kms.katalon.core.testcase.TestCase as TestCase
import com.kms.katalon.core.testdata.TestData as TestData
import com.kms.katalon.core.testng.keyword.TestNGBuiltinKeywords as TestNGKW
import com.kms.katalon.core.testobject.TestObject as TestObject
import com.kms.katalon.core.webservice.keyword.WSBuiltInKeywords as WS
import com.kms.katalon.core.webui.keyword.WebUiBuiltInKeywords as WebUI
import com.kms.katalon.core.windows.keyword.WindowsBuiltinKeywords as Windows
import internal.GlobalVariable as GlobalVariable
import org.openqa.selenium.Keys as Keys

WebUI.openBrowser('')

WebUI.navigateToUrl('http://192.168.0.23:8080/oem-dev/faces/portal/login.jsf?faces-redirect=true')

WebUI.setText(findTestObject('Object Repository/Page_ HC System Login/input_Login_formLoginusername'), '21000003')

WebUI.setEncryptedText(findTestObject('Object Repository/Page_ HC System Login/input_Login_formLoginpassword'), 'xbXHGMmSqt8=')

WebUI.click(findTestObject('Object Repository/Page_ HC System Login/span_Masuk'))

WebUI.click(findTestObject('Object Repository/Page_OEM - Dashboard/a_Leave'))

WebUI.scrollToElement(findTestObject('Page_OEM - Dashboard/span_Upload Leave Adjustment'), 0)

WebUI.click(findTestObject('Page_OEM - Dashboard/span_Upload Leave Adjustment'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/span_Add'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/input__formstartDateInput_input'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Previous'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Previous'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_1'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/input__formendDateInput_input'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/td_31'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/label_'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/li_Cuti Tahunan 2021'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/label__1'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/li_Additional'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/input_Yes_formj_idt122'))

WebUI.uploadFile(findTestObject('Leave_Adjustments_Repo/Add_Repo/input__uploadFile'), 'C://Users/user/Desktop/test.xlsx')

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/span_Save To Draft'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/span_Add'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/input__formstartDateInput_input'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Previous'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Previous'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_1'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/input__formendDateInput_input'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/a_Next'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/td_31'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/label_'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/li_Cuti Tahunan 2021'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/label__1'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/li_Additional'))

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/input__formj_idt122'))

WebUI.uploadFile(findTestObject('Leave_Adjustments_Repo/Add_Repo/input__uploadFile'), 'C://Users/user/Desktop/test.xlsx')

WebUI.click(findTestObject('Leave_Adjustments_Repo/Add_Repo/span_Save To Draft'))

WebUI.closeBrowser()

