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

WebUI.navigateToUrl('http://localhost:22083/Home')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Page - Dashboard/a_Master'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Page - Dashboard/a_WA0AUC04'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Add'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt2'), 'K')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt4'), '1TR')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt5'), '2')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_sc_TC_From'), '20210310')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_sc_TC_To'), '20210311')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Line Code should not be empty', true)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt3'), 'AKK3K1')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt5'), '')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Capacity should not be empty', true)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/td_Created Date_TcFrom'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_sc_TC_From'), '')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt5'), '2')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Created Date_btnSave'))

WebUI.verifyTextPresent('TC From should not be empty', true)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt4'), 'asd')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_sc_TC_From'), '20210310')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Heijunka Code with Company Code 2000 not exist on Heijunkan Code Name Master', true)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt4'), '    ')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Created Date_btnSave'))

arg1 = WebUI.getAttribute(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt4'), 
    'value')

arg2 = '-'

WebUI.verifyEqual(arg1, arg2, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt4'), '4WD')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt5'), '4')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_sc_TC_From'), '20210401')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_sc_TC_To'), '20210418')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Overlap found for T/C From and T/C to in records with the same Line Code and Heijunka Code', true)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_sc_TC_From'), '20210427')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Created Date_btnSave'))

WebUI.verifyTextPresent('TC From cannot be greater than TC To', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt4'), '1TR')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_sc_TC_From'), '20210401')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Process Save Capacity Master finish successfully', true)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Add'))

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Created Date_cancelSubmit'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Created Date_cancelSubmit'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Add'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt2'), 'M')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt3'), 'AKK3M1')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt4'), '1TR')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt5'), '245')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_sc_TC_From'), '20210325')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_sc_TC_To'), '20210326')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Created Date_btnSave'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Add'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt2'), 'M')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt3'), 'AKK3M1')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt4'), '2NR')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt5'), '323')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_sc_TC_From'), '20210323')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_sc_TC_To'), '20210324')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Created Date_btnSave'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Add'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt2'), 'M')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt3'), 'AKK3M1')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt4'), '4WD')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_txt5'), '567')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_sc_TC_From'), '20210327')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/input_Created Date_sc_TC_To'), '20210328')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Add/button_Created Date_btnSave'))

WebUI.closeBrowser()

