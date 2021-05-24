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

WebUI.setText(findTestObject('Page_ HC System Login/input_Login_formLoginusername'), '21000003')

WebUI.setEncryptedText(findTestObject('Page_ HC System Login/input_Login_formLoginpassword'), 'xbXHGMmSqt8=')

WebUI.click(findTestObject('Object Repository/Page_ HC System Login/span_Masuk'))

WebUI.click(findTestObject('Page_OEM - Dashboard/a_Leave'))

WebUI.scrollToElement(findTestObject('Page_OEM - Dashboard/span_Upload Leave Adjustment'), 0)

WebUI.click(findTestObject('Page_OEM - Dashboard/span_Upload Pengajuan Carry Over Cuti'))

WebUI.setText(findTestObject('Leave_Carry_Over_Upload_Repo/Search_Repo/input_Upload By_formsearchUploadBy'), '21000003 - Lim Neir Kate')

arg1 = WebUI.getAttribute(findTestObject('Leave_Carry_Over_Upload_Repo/Search_Repo/input_Upload By_formsearchUploadBy'), 
    'value')

arg2 = WebUI.getText(findTestObject('Leave_Carry_Over_Upload_Repo/Search_Repo/td_21000003 - Lim Neir Kate'))

WebUI.click(findTestObject('Leave_Carry_Over_Upload_Repo/Search_Repo/span_Search'))

WebUI.verifyEqual(arg1, arg2)

WebUI.click(findTestObject('Leave_Carry_Over_Upload_Repo/Search_Repo/input_Upload Date_formsearchUploadDateFrom_input'))

WebUI.click(findTestObject('Leave_Carry_Over_Upload_Repo/Search_Repo/a_16'))

arg3 = WebUI.getAttribute(findTestObject('Leave_Carry_Over_Upload_Repo/Search_Repo/input_Upload Date_formsearchUploadDateFrom_input'), 
    'value')

arg4 = WebUI.getText(findTestObject('Leave_Carry_Over_Upload_Repo/Search_Repo/td_16-Mar-2021'))

WebUI.click(findTestObject('Leave_Carry_Over_Upload_Repo/Search_Repo/span_Search'))

WebUI.verifyEqual(arg3, arg4)

WebUI.setText(findTestObject('Leave_Carry_Over_Upload_Repo/Search_Repo/input_Upload By_formsearchUploadBy'), '21000003 - Lim Neir Kate')

WebUI.click(findTestObject('Leave_Carry_Over_Upload_Repo/Search_Repo/input_Upload Date_formsearchUploadDateFrom_input'))

WebUI.click(findTestObject('Leave_Carry_Over_Upload_Repo/Search_Repo/a_16'))

WebUI.click(findTestObject('Leave_Carry_Over_Upload_Repo/Search_Repo/span_Search'))

WebUI.verifyEqual(arg1, arg2)

WebUI.verifyEqual(arg3, arg4)

WebUI.closeBrowser()

