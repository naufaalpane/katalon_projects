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

WebUI.selectOptionByValue(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/select_Choose                              _8b7d21'), 
    'k', true)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/input_Summary HEIJUNKA Code  _hjkcd'), 
    'asd')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/button_Search'))

WebUI.verifyTextPresent('an error message', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/input_Line Code _linecd'), 
    'asd')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/button_Search'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/td_No Data Found'), 'No Data Found')

WebUI.selectOptionByValue(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/select_Choose                              _8b7d21'), 
    '', true)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/button_Search'))

WebUI.verifyTextPresent('an error message', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/td_No Data Found'), 'No Data Found')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/input_Line Code _linecd'), 
    '')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/input_Summary HEIJUNKA Code  _hjkcd'), 
    '')

WebUI.selectOptionByValue(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/select_Choose                              _8b7d21'), 
    'm', true)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/button_Search'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/Data_M/td_M'), 'M')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/Data_M/td_M_2'), 'M')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/Data_M/td_M_3'), 'M')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/Data_M/td_M_4'), 'M')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/Data_M/td_M_5'), 'M')

WebUI.selectOptionByValue(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/select_Choose                              _8b7d21'), 
    'k', true)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/button_Search'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/Data_K/td_K'), 'K')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/Data_K/td_K_2'), 'K')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/input_Line Code _linecd'), 
    'AKK3K1')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/input_Summary HEIJUNKA Code  _hjkcd'), 
    '1TR')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/button_Search'))

arg1 = WebUI.getAttribute(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/select_Choose                              _8b7d21'), 
    'value')

arg2 = WebUI.getText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/Validate/td_K'))

if (arg2 == 'K') {
    arg2 = 'k'
}

arg3 = WebUI.getAttribute(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/input_Line Code _linecd'),
	'value')

arg4 = WebUI.getText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/Validate/td_AKK3K1'))

arg5 = WebUI.getAttribute(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/input_Summary HEIJUNKA Code  _hjkcd'),
	'value')

arg6 = WebUI.getText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Search/Validate/td_1TR'))

WebUI.verifyEqual(arg1, arg2)

WebUI.verifyEqual(arg3, arg4)

WebUI.verifyEqual(arg5, arg6)

WebUI.closeBrowser()

