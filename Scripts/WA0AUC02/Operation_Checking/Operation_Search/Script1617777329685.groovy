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

WebUI.maximizeWindow()

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Page_Dashboard/a_Master'))

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Page_Dashboard/a_WA0AUC02'))

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Buttons/button_Search'))

WebUI.verifyTextPresent('Please Input Data', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Car Family Code _cfc'), '272W')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Parts No. _partsno'), '12000')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_-_partsno'), '0C020')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_-_partsno_1'), '00')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Line Code _linecd'), 'AKS2K1')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Buttons/button_Search'))

arg1 = WebUI.getAttribute(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Car Family Code _cfc'), 
    'value')

arg2a = WebUI.getAttribute(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Parts No. _partsno'), 
    'value')

arg2b = WebUI.getAttribute(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_-_partsno'), 'value')

arg2c = WebUI.getAttribute(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_-_partsno_1'), 'value')

arg2 = ((((arg2a + '-') + arg2b) + '-') + arg2c)

arg3 = WebUI.getAttribute(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Line Code _linecd'), 
    'value')

arg4 = WebUI.getText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Search/Table_Contents/td_272W'))

arg5 = WebUI.getText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Search/Table_Contents/td_12000-0C020-00'))

arg6 = WebUI.getText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Search/Table_Contents/td_AKS2K1'))

WebUI.verifyEqual(arg1, arg4)

WebUI.verifyEqual(arg2, arg5)

WebUI.verifyEqual(arg3, arg6)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Line Code _linecd'), 'ASD')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Buttons/button_Search'))

WebUI.verifyTextPresent('Please Input Correct Line Code', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Line Code _linecd'), 'AKS2K1')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Car Family Code _cfc'), 'ASD')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Buttons/button_Search'))

WebUI.verifyTextPresent('Please Input Correct Family Code', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Car Family Code _cfc'), '272W')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Parts No. _partsno'), '123')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_-_partsno'), '123')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_-_partsno_1'), '123')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Buttons/button_Search'))

WebUI.verifyTextPresent('Please Input Correct Parts Number', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Buttons/button_Clear'))

WebUI.selectOptionByValue(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/select_3                5                10_e5027c'), 
    '5', true)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_checkall'), FailureHandling.STOP_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_1'), 
    30)

WebUI.verifyElementChecked(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_1'), 
    30)

WebUI.verifyElementChecked(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_1'), 
    30)

WebUI.verifyElementChecked(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_1'), 
    30)

WebUI.verifyElementChecked(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_1'), 
    30)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_checkall'), FailureHandling.STOP_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_1'), FailureHandling.STOP_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_1'), 
    30)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Car Family Code _cfc'), '272W')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Parts No. _partsno'), '12000')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_-_partsno'), '0C020')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_-_partsno_1'), '00')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Buttons/button_Search'))

WebUI.verifyTextPresent('Line Code Must Be Filled', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Line Code _linecd'), 'AKS2K1')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Car Family Code _cfc'), '')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Buttons/button_Search'))

WebUI.verifyTextPresent('Family Code Must Be Filled', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Car Family Code _cfc'), '272W')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_Parts No. _partsno'), '')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_-_partsno'), '')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Textboxes/input_-_partsno_1'), '')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Buttons/button_Search'))

WebUI.verifyTextPresent('Parts Number Must Be Filled', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Buttons/button_Clear'))

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Buttons/i_Created Date_fa fa-trash'))

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Search/Buttons/button_Yes'))

WebUI.closeBrowser()

