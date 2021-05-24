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

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/a_Master'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/a_WA0AUC04'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/i_dummy_fa fa-pen'))

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/i_dummy_fa fa-times'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/input_dummy_txt3'), '', FailureHandling.CONTINUE_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/tr_dummy_editLine'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/input_dummy_txt5'), '')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/i_dummy_fa fa-save'))

WebUI.verifyTextPresent('Capacity should not be empty', true)

WebUI.delay(5)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/input_dummy_txt5'), '234,000000')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/tr_dummy_editLine'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/input_dummy_sc_TC_From'), '')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/i_dummy_fa fa-save'))

WebUI.verifyTextPresent('TC From should not be empty', true)

WebUI.delay(5)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/input_dummy_txt4'), '     ', FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/input_dummy_sc_TC_From'), '20210416')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/input_dummy_sc_TC_To'), '20210418')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/i_dummy_fa fa-save'))

arg1 = WebUI.getAttribute(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/input_dummy_txt4'), 'value')

arg2 = '-'

WebUI.verifyEqual(arg1, arg2, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyTextPresent('Overlap found for T/C From and T/C to in records with the same Line Code and Heijunka Code', true)

WebUI.delay(5)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/input_dummy_sc_TC_From'), '20210419')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/i_dummy_fa fa-save'))

WebUI.verifyTextPresent('TC From cannot be greater than TC To', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.delay(5)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/input_dummy_txt5'), '123123')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/input_dummy_sc_TC_From'), '20210422')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/input_dummy_sc_TC_To'), '20210423')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/i_dummy_fa fa-save'))

WebUI.verifyTextPresent('Process Save Capacity Master finish successfully', true)

WebUI.delay(5)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/i_dummy_fa fa-pen'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Edit/i_dummy_fa fa-times'))

WebUI.verifyTextPresent('Are you sure you want to edit this data?', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.closeBrowser()

