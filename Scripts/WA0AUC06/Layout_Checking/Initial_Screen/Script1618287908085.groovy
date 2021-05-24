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

WebUI.maximizeWindow()

WebUI.navigateToUrl('http://localhost:22083/Home')

WebUI.enhancedClick(findTestObject('WA0AUC06_Repo/Page_Dashboard/a_Master'))

WebUI.enhancedClick(findTestObject('WA0AUC06_Repo/Page_Dashboard/a_WA0AUC06'))

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Label/lable_WA0AUC06 - Stock Level Master Settings'), 
    'WA0AUC06 - Stock Level Master Settings')

WebUI.verifyElementClickable(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Combobox/select_Process'))

WebUI.verifyElementClickable(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Combobox/select_Status Code'))

WebUI.verifyElementClickable(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Combobox/select_Size'))

WebUI.setText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Textbox/input_Line Code _txtSearchLineCode'), 
    '')

WebUI.setText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Textbox/input_Car Family Code _txtSearchCarFamilyCode'), 
    '')

WebUI.setText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Textbox/input_Parts No. _partsno'), '')

WebUI.setText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Textbox/input_-_partsno'), '')

WebUI.setText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Textbox/input_-_partsno_1'), '')

WebUI.setText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Textbox/input_Last_gotovalue'), '')

WebUI.verifyElementClickable(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/button_Upload'))

WebUI.verifyElementClickable(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/button_Download'))

WebUI.verifyElementClickable(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/button_Clear'))

WebUI.verifyElementClickable(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/button_Search'))

WebUI.verifyElementClickable(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/button_Go'))

WebUI.verifyElementClickable(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/button_Add'))

WebUI.verifyElementClickable(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/button_Delete'))

WebUI.verifyElementNotChecked(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_checkall'), 
    30)

WebUI.verifyElementNotChecked(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow'), 
    30)

WebUI.verifyElementNotChecked(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_2'), 
    30)

WebUI.verifyElementNotChecked(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_3'), 
    30)

WebUI.verifyElementNotChecked(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_4'), 
    30)

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_Action'), 'Action')

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_Process Code'), 
    'Process Code')

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_Line Code'), 
    'Line Code')

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_Car Family Code'), 
    'Car Family Code')

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_Part No'), 'Part No')

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_Status Code'), 
    'Status Code')

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_Export Code'), 
    'Export Code')

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_Part Name'), 
    'Part Name')

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_Unit Sign'), 
    'Unit Sign')

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_Min Stock'), 
    'Min Stock')

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_Max Stock'), 
    'Max Stock')

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_TC (Form)'), 
    'T/C (Form)')

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_TC (To)'), 'T/C (To)')

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_Created By'), 
    'Created By', FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementText(findTestObject('WA0AUC06_Repo/Layout_Checking_Repo/Initial_Screen/Table_Headers/th_Created Date'), 
    'Created Date', FailureHandling.CONTINUE_ON_FAILURE)

WebUI.closeBrowser()

