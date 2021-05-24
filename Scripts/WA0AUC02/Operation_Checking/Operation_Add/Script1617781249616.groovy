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

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/button_Add'))

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Car Family Code should not be empty', true)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_Created Date_partsno1'), '12000')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_partsno12'), '0C020')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_partsno13'), '00')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt4'), 'AKS2K1')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt5'), '1')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt6'), 'Engine 2')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt7'), 'TEST1')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_sc_TC_From'), '20210401')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_sc_TC_To'), '20210403')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/i_Created Date_fa fa-save'))

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_Created Date_txt2'), '272W')

WebUI.clearText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_Created Date_partsno1'), FailureHandling.STOP_ON_FAILURE)

WebUI.clearText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_partsno12'), FailureHandling.STOP_ON_FAILURE)

WebUI.clearText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_partsno13'), FailureHandling.STOP_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('First Part No. should not be empty', true)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_Created Date_partsno1'), '12000')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_partsno12'), '0C020')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_partsno13'), '00')

WebUI.clearText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt4'), FailureHandling.STOP_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Line Code should not be empty', true)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt4'), 'AKS2K1')

WebUI.clearText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt5'), FailureHandling.STOP_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Status Code should not be empty', true)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt5'), '1')

WebUI.clearText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt6'), FailureHandling.STOP_ON_FAILURE)

WebUI.clearText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt7'), FailureHandling.STOP_ON_FAILURE)

WebUI.verifyTextPresent('Parts Name should not be empty', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyTextPresent('Unit Sign should not be empty', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/i_Created Date_fa fa-save'))

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/i_Created Date_fa fa-trash'))

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/button_Yes'))

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/button_Add'))

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_Created Date_txt2'), '272W')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_Created Date_partsno1'), '12000')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_partsno12'), '0C020')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_partsno13'), '00')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt4'), 'AKS2K1')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt5'), '1')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt6'), 'Engine 2')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt7'), 'TEST1')

WebUI.clearText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_sc_TC_From'), FailureHandling.STOP_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_sc_TC_To'), '20210403')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('TC From should not be empty', true)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_sc_TC_From'), '20210401')

WebUI.clearText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_sc_TC_To'), FailureHandling.STOP_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('TC To should not be empty', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_sc_TC_To'), '20210403')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Process Save Unit Production Control Master finish successfully', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/button_Add'))

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_Created Date_txt2'), '272W')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_Created Date_partsno1'), '12000')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_partsno12'), '0C020')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_partsno13'), '00')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt4'), 'AKS2K1')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt5'), '1')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt6'), 'Engine 2')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_txt7'), 'TEST1')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_sc_TC_From'), '20210401')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Textboxes/input_-_sc_TC_To'), '20210403')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Add/Buttons/i_Created Date_fa fa-times'))

WebUI.closeBrowser()

