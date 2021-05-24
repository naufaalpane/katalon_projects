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

WebUI.setText(findTestObject('Object Repository/Page_ HC System Login/input_Login_formLoginusername'), '21000014')

WebUI.setEncryptedText(findTestObject('Object Repository/Page_ HC System Login/input_Login_formLoginpassword'), 'xbXHGMmSqt8=')

WebUI.click(findTestObject('Object Repository/Page_ HC System Login/span_Masuk'))

WebUI.click(findTestObject('Object Repository/Leave_Perencanaan_Cuti_Repo/Page_Leave - Plan/a_MANAGER SELF SERVICE'))

WebUI.click(findTestObject('Object Repository/Leave_Perencanaan_Cuti_Repo/Page_Leave - Plan/a_Leave'))

WebUI.click(findTestObject('Object Repository/Leave_Perencanaan_Cuti_Repo/Page_Leave - Plan/a_Persetujuan Perencanaan Cuti'))

WebUI.click(findTestObject('Object Repository/Leave_Perencanaan_Cuti_Repo/Page_Leave - Carry Over/span_Approve'))

WebUI.click(findTestObject('Leave_Perencanaan_Cuti_Repo/Page_Leave - Carry Over/span_Back'))

WebUI.click(findTestObject('Object Repository/Leave_Perencanaan_Cuti_Repo/Page_Leave - Carry Over/span_Approve'))

WebUI.click(findTestObject('Leave_Perencanaan_Cuti_Repo/Page_Leave - Carry Over/span_Reject'))

WebUI.click(findTestObject('Leave_Perencanaan_Cuti_Repo/Page_Leave - Carry Over/Approval/span_Yes'))

WebUI.click(findTestObject('Object Repository/Leave_Perencanaan_Cuti_Repo/Page_Leave - Carry Over/span_Approve'))

WebUI.click(findTestObject('Leave_Perencanaan_Cuti_Repo/Page_Leave - Carry Over/Approval/span_Approve'))

WebUI.click(findTestObject('Leave_Perencanaan_Cuti_Repo/Page_Leave - Carry Over/span_Yes'))

WebUI.click(findTestObject('Object Repository/Leave_Perencanaan_Cuti_Repo/Page_Leave - Carry Over/span_Approve'))

WebUI.click(findTestObject('Leave_Perencanaan_Cuti_Repo/Page_Leave - Carry Over/Approval/span_Approve'))

WebUI.click(findTestObject('Leave_Perencanaan_Cuti_Repo/new/span_Yes'))

WebUI.closeBrowser()

