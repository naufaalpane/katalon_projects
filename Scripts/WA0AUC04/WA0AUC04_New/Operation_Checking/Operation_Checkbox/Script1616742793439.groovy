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

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Opertaion_Checkbox/input_Delete_checkall'))

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Opertaion_Checkbox/input_chkRow'), 
    0, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Opertaion_Checkbox/input_chkRow_2'), 
    0, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Opertaion_Checkbox/input_chkRow_3'), 
    0, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Opertaion_Checkbox/input_chkRow_4'), 
    0, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Opertaion_Checkbox/input_chkRow_5'), 
    0, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Opertaion_Checkbox/input_chkRow_6'), 
    0, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Opertaion_Checkbox/input_chkRow_7'), 
    0, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Opertaion_Checkbox/input_chkRow_8'), 
    0, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Opertaion_Checkbox/input_chkRow'))

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_New_Repo/Operation_Checking_Repo/Opertaion_Checkbox/input_chkRow'), 
    0)

WebUI.closeBrowser()

