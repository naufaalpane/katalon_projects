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

WebUI.setText(findTestObject('Login/input_Login_frmLoginj_idt10'), 'admin')

WebUI.setEncryptedText(findTestObject('Login/input_Login_frmLoginj_idt12'), 'RAIVpflpDOg=')

WebUI.click(findTestObject('Province_Repo/Delete/span_Login'))

WebUI.click(findTestObject('Dashboard/a_Master'))

WebUI.click(findTestObject('Dashboard/a_Province'))

WebUI.click(findTestObject('Province_Repo/Delete/span_2'))

WebUI.click(findTestObject('Province_Repo/Delete/span_Jawa Tengah_ui-chkbox-icon ui-icon ui-_fcf689'))

WebUI.click(findTestObject('Province_Repo/Delete/span_Hapus'))

WebUI.click(findTestObject('Province_Repo/Delete/span_Yes'))

WebUI.verifyTextNotPresent('test', true)

WebUI.verifyTextNotPresent('testtesting', true)

WebUI.click(findTestObject('Province_Repo/Delete/span_1'))

WebUI.verifyTextNotPresent('test', true)

WebUI.verifyTextNotPresent('testtesting', true)

WebUI.closeBrowser()

