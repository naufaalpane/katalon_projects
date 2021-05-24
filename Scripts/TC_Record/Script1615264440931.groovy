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

WebUI.navigateToUrl('https://www.thetestingworld.com/testings/')

WebUI.setText(findTestObject('TC_Record_Result/input_Address type HomeOffice_fld_username'), 'bebekebo')

WebUI.setText(findTestObject('TC_Record_Result/input_Address type HomeOffice_fld_email'), findTestData('TC_Data').getValue('Email', 1))

WebUI.setEncryptedText(findTestObject('TC_Record_Result/input_Address type HomeOffice_fld_password'),  findTestData('TC_Data').getValue('Password', 1))

WebUI.setEncryptedText(findTestObject('TC_Record_Result/input_Address type HomeOffice_fld_cpassword'),  findTestData('TC_Data').getValue('Confirm Password', 1))

WebUI.click(findTestObject('TC_Record_Result/input_Address type HomeOffice_dob'))

WebUI.click(findTestObject('TC_Record_Result/a_18'))

WebUI.setText(findTestObject('TC_Record_Result/input_Address type HomeOffice_phone'), '087788771866')

WebUI.setText(findTestObject('TC_Record_Result/input_Address type HomeOffice_address'), 'jakarta')

WebUI.click(findTestObject('TC_Record_Result/input_Address type HomeOffice_add_type'))

WebUI.selectOptionByValue(findTestObject('TC_Record_Result/select_Choose Gender                  Male _4c15ff'), '1', true)

WebUI.selectOptionByValue(findTestObject('TC_Record_Result/select_Select CountryAfghanistanAlbaniaAlge_9a933d'), '102', 
    true)

WebUI.click(findTestObject('TC_Record_Result/input_Address type HomeOffice_terms'))

WebUI.closeBrowser()

