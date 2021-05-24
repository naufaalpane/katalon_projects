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

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/lable_WA0AUC04 - K  M Capacity Master Set Up'), 
    'WA0AUC04 - K / M Capacity Master Set Up')

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/select_Choose'))

WebUI.selectOptionByValue(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/select_3                5                10_e5027c'), 
    '3', true)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/input_Line Code _linecd'), 
    '')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/input_Summary HEIJUNKA Code  _hjkcd'), 
    '')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/input_Last_gotovalue'), 
    '')

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/button_Clear'), 
    FailureHandling.STOP_ON_FAILURE)

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/button_Search'))

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/button_Upload'), 
    FailureHandling.STOP_ON_FAILURE)

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/button_Download'))

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/button_Add'))

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/button_Delete'))

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/a_First'))

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/a_Prev'))

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/a_Next'))

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/a_Last'))

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/button_Go'))

WebUI.verifyElementNotChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/input_Delete_checkall'), 
    0)

WebUI.verifyElementNotChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/input_Created Date_chkRow'), 
    0)

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/th_Action'), 
    'Action')

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/button_Created Date_editLine'))

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/button_Created Date_copyLine'))

WebUI.verifyElementClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/button_Created Date_btn initialBtn btn-md btn-outline-warning pt-0 pb-0 pl-1 pr-1'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/th_Process Code'), 
    'Process Code')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/th_Line Code'), 
    'Line Code')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/th_Summary HEIJUNKA Code'), 
    'Summary HEIJUNKA Code')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/th_Capacity  Day'), 
    'Capacity / Day')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/th_TC (from)'), 
    'T/C (from)')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Layout_Checking_Repo/Initial_Display/th_TC (to)'), 
    'T/C (to)')

WebUI.verifyTextNotPresent('Created By', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyTextNotPresent('Created Date', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.closeBrowser()

