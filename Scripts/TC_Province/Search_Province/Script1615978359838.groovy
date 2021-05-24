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

WebUI.navigateToUrl('http://192.168.0.23:8080/epos/faces/login/dashboard.jsf')

WebUI.setText(findTestObject('Object Repository/Login/input_Login_frmLoginj_idt10'), 'admin')

WebUI.setEncryptedText(findTestObject('Object Repository/Login/input_Login_frmLoginj_idt12'), 'RAIVpflpDOg=')

WebUI.click(findTestObject('Object Repository/Province_Repo/Delete/span_Login'))

WebUI.click(findTestObject('Dashboard/a_Master'))

WebUI.click(findTestObject('Dashboard/a_Province'))

WebUI.setText(findTestObject('Province_Repo/Search/Page_EPOS/input_Provinsi_frm001searchAll'), 'test')

WebUI.click(findTestObject('Province_Repo/Search/Page_EPOS/span_ui-button_ui-button-icon-left ui-icon _80f6e7'))

arg1 = WebUI.getAttribute(findTestObject('Province_Repo/Search/Page_EPOS/input_Provinsi_frm001searchAll'), 'value')

arg2 = WebUI.getText(findTestObject('Object Repository/Province_Repo/Search/Page_EPOS/td_test'))

WebUI.verifyEqual(arg1, arg2)

WebUI.click(findTestObject('Province_Repo/Search/Page_EPOS/span_Provinsi_ui-button-icon-left ui-icon u_e6058a'))

WebUI.click(findTestObject('Province_Repo/Search/Page_EPOS/input_Provinsi_frm001searchAll'))

WebUI.click(findTestObject('Province_Repo/Search/Page_EPOS/span_ui-button_ui-button-icon-left ui-icon _529497'))

WebUI.setText(findTestObject('Province_Repo/Search/Page_EPOS/input_Kode Provinsi_frm001j_idt78'), 'a')

WebUI.setText(findTestObject('Province_Repo/Search/Page_EPOS/input_Nama Provinsi_frm001j_idt82'), 'b')

WebUI.click(findTestObject('Province_Repo/Search/Page_EPOS/span_Bersihkan'))

WebUI.setText(findTestObject('Province_Repo/Search/Page_EPOS/input_Kode Provinsi_frm001j_idt78'), 'test')

WebUI.setText(findTestObject('Province_Repo/Search/Page_EPOS/input_Nama Provinsi_frm001j_idt82'), 'testtesting')

WebUI.click(findTestObject('Province_Repo/Search/Page_EPOS/span_Cari'))

arg3 = WebUI.getAttribute(findTestObject('Province_Repo/Search/Page_EPOS/input_Provinsi_frm001searchAll'), 'value')

arg4 = WebUI.getText(findTestObject('Object Repository/Province_Repo/Search/Page_EPOS/td_test'))

