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

WebUI.selectOptionByValue(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/select_3                5                10_e5027c'), 
    '3', true)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/a_Next'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_2/td_AKK3M1'), 
    'AKK3M1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_2/td_AKK3M1_2'), 
    'AKK3M1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_2/td_AKK3M2'), 
    'AKK3M2')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/a_First'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_1/td_AKK3K1'), 
    'AKK3K1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_1/td_AKK3K1'), 
    'AKK3K1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_1/td_AKK3M1'), 
    'AKK3M1')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/a_Last'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_3/td_AKK3M3'), 
    'AKK3M3')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_3/td_AKK3M4'), 
    'AKK3M4')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/a_Prev'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_2/td_AKK3M1'), 
    'AKK3M1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_2/td_AKK3M1_2'), 
    'AKK3M1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_2/td_AKK3M2'), 
    'AKK3M2')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/button_Go'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_2/td_AKK3M1'), 
    'AKK3M1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_2/td_AKK3M1_2'), 
    'AKK3M1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_2/td_AKK3M2'), 
    'AKK3M2')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/input_Last_gotovalue'), 
    '1')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/button_Go'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_1/td_AKK3K1'), 
    'AKK3K1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_1/td_AKK3K1'), 
    'AKK3K1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Operation_Table_Button/Page_1/td_AKK3M1'), 
    'AKK3M1')

WebUI.closeBrowser()

