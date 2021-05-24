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

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/button_dummy_copyLine'))

WebUI.verifyElementNotClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/Disabled_Buttons/button_Add'))

WebUI.verifyElementNotClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/Disabled_Buttons/button_Search'))

WebUI.verifyElementNotClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/Disabled_Buttons/button_Delete'))

WebUI.verifyElementNotClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/Disabled_Buttons/button_Clear'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/button_Created Date_cancelSubmit'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/button_dummy_copyLine'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/input_Created Date_txt4'), '2NR')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Overlap found for T/C From and T/C to in records with the same Line Code and Heijunka Code', true)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/input_Created Date_txt4'), '4WD')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/button_Created Date_btnSave'))

WebUI.verifyTextPresent('ERROR: Data is already exists with key Line Code', true)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/input_Created Date_txt4'), 'AT')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Heijunka Code with Company Code 2000 not exist on Heijunkan Code Name Master', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/input_Created Date_txt4'), '1TR')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/button_Created Date_btnSave'))

WebUI.verifyTextNotPresent('ERROR: SP_M_Capacity_Master_Setup_SaveAddEdit: Error converting data type varchar to numeric., at line = 154', 
    true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/button_Created Date_cancelSubmit'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/i_Created Date_fa fa-trash'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/temp/button_Yes'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/temp/button_Created Date_copyLine'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/temp/input_Created Date_txt4'), '1TR')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/temp/input_Created Date_txt5'), '2')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/temp/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Process Save Capacity Master finish successfully', true)

WebUI.closeBrowser()

