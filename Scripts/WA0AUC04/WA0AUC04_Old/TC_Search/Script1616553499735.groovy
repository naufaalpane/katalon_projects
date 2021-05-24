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

WebUI.navigateToUrl('http://localhost:22083/MCapacityMasterSetUp')

WebUI.selectOptionByValue(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/select_Choose                              _8b7d21'), 
    'm', true)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/button_Search'))

arg1 = WebUI.getAttribute(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/select_Choose                              _8b7d21'), 
    'value')

arg2 = WebUI.getText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/Table_Data/td_M'))

if (arg2 == 'M') {
    arg2 = 'm'
}

WebUI.verifyEqual(arg1, arg2)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/button_Clear'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Line Code _linecd'), 'AKK3M2')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/button_Search'))

arg3 = WebUI.getAttribute(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Line Code _linecd'), 'value')

arg4 = WebUI.getText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/Table_Data/td_AKK3M2'))

WebUI.verifyEqual(arg1, arg2)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/button_Clear'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Summary HEIJUNKA Code  _hjkcd'), '1TR')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/button_Search'))

arg5 = WebUI.getAttribute(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Summary HEIJUNKA Code  _hjkcd'), 
    'value')

arg6 = WebUI.getText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/Table_Data/td_1TR'))

WebUI.verifyEqual(arg5, arg6)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/button_Clear'))

WebUI.selectOptionByValue(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/select_Choose                              _8b7d21'), 
    'm', true)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Line Code _linecd'), 'AKK3M2')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Summary HEIJUNKA Code  _hjkcd'), '1TR')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/button_Search'))

WebUI.verifyEqual(arg1, arg2)

WebUI.verifyEqual(arg3, arg4)

WebUI.verifyEqual(arg5, arg6)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Line Code _linecd'), 'asd')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/button_Search'))

WebUI.verifyEqual(arg3, arg4)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/button_Clear'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Summary HEIJUNKA Code  _hjkcd'), 'asd')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/button_Search'))

WebUI.verifyEqual(arg5, arg6)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/button_Clear'))

WebUI.selectOptionByValue(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/select_Choose                              _8b7d21'), 
    'm', true)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Line Code _linecd'), 'asd')

arg7 = WebUI.getAttribute(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Line Code _linecd'), 'value')

arg8 = WebUI.getText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/Table_Data/td_No Data Found'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Summary HEIJUNKA Code  _hjkcd'), 'asd')

arg9 = WebUI.getAttribute(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Summary HEIJUNKA Code  _hjkcd'), 
    'value')

arg10 = WebUI.getText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/Table_Data/td_No Data Found'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/button_Search'))

WebUI.verifyNotEqual(arg7, arg8)

WebUI.verifyNotEqual(arg9, arg10)

WebUI.closeBrowser()

