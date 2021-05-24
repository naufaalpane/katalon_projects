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

WebUI.setText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Textboxes/input_Car Family Code _cfc'), 
    '')

WebUI.setText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Textboxes/input_Parts No. _partsno'), '')

WebUI.setText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Textboxes/input_-_partsno'), '')

WebUI.setText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Textboxes/input_-_partsno_1'), '')

WebUI.setText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Textboxes/input_Line Code _linecd'), '')

WebUI.verifyElementClickable(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/button_Go'))

WebUI.verifyElementClickable(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/button_Clear'))

WebUI.verifyElementClickable(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/button_Add'))

WebUI.verifyElementClickable(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/button_Delete'))

WebUI.verifyElementClickable(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/button_Search'))

WebUI.verifyElementClickable(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/select_3                5                10_e5027c'))

WebUI.verifyElementNotChecked(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_checkall'), 
    30)

WebUI.verifyElementNotChecked(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_1'), 
    30)

WebUI.verifyElementNotChecked(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_1'), 
    30)

WebUI.verifyElementNotChecked(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_1'), 
    30)

WebUI.verifyElementNotChecked(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_1'), 
    30)

WebUI.verifyElementNotChecked(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Checkboxes/input_chkRow_1'), 
    30)

WebUI.verifyElementText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Table_Contents/th_Action'), 'Action')

WebUI.verifyElementText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Table_Contents/th_Car Family Code'), 
    'Car Family Code')

WebUI.verifyElementText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Table_Contents/th_Parts No'), 
    'Parts No.')

WebUI.verifyElementText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Table_Contents/th_Line Code'), 
    'Line Code')

WebUI.verifyElementText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Table_Contents/th_Status Code'), 
    'Status Code')

WebUI.verifyElementText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Table_Contents/th_Parts Name'), 
    'Parts Name')

WebUI.verifyElementText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Table_Contents/th_Unit Sign'), 
    'Unit Sign')

WebUI.verifyElementText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Table_Contents/th_TC (from)'), 
    'T/C (from)')

WebUI.verifyElementText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Table_Contents/th_TC (to)'), 'T/C (to)')

WebUI.verifyElementText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Table_Contents/th_Created By'), 
    'Created By')

WebUI.verifyElementText(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Table_Contents/th_Created Date'), 
    'Created Date')

WebUI.verifyElementClickable(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/i_Created Date_fa fa-pen'))

WebUI.verifyElementClickable(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/i_Created Date_fa fa-copy'))

WebUI.verifyElementClickable(findTestObject('WA0AUC02_Repo/Layout_Checking_Repo/Initial_Screen/Buttons/i_Created Date_fa fa-trash'))

WebUI.closeBrowser()

