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

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-pen'), FailureHandling.STOP_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Disabled_Textboxes/input_Created Date_txt2'), '', 
    FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Disabled_Textboxes/input_Created Date_partsno1'), 
    '', FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Disabled_Textboxes/input_-_partsno12'), '', FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Disabled_Textboxes/input_-_partsno13'), '', FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Disabled_Textboxes/input_-_txt4'), '', FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Disabled_Textboxes/input_-_txt5'), '', FailureHandling.CONTINUE_ON_FAILURE)

WebUI.clearText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_txt6'), FailureHandling.STOP_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Please Fill Out Parts Name', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-pen'))

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_txt6'), 'Engine 2')

WebUI.clearText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_txt7'), FailureHandling.STOP_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Please Fill Out Unit Sign', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-pen'))

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_txt7'), 'TEST1')

WebUI.clearText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_sc_TC_From'), FailureHandling.STOP_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('TC From should not be empty', true)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_sc_TC_From'), '20210401')

WebUI.clearText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_sc_TC_To'), FailureHandling.STOP_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('TC To should not be empty', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_sc_TC_To'), '20210403')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-times'))

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-pen'), FailureHandling.STOP_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Disabled_Textboxes/input_Created Date_txt2'), '', 
    FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Disabled_Textboxes/input_Created Date_partsno1'), 
    '', FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Disabled_Textboxes/input_-_partsno12'), '', FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Disabled_Textboxes/input_-_partsno13'), '', FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Disabled_Textboxes/input_-_txt4'), '', FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Disabled_Textboxes/input_-_txt5'), '', FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_txt6'), 'asd')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Please Fill Out Parts Name', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-pen'))

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_txt6'), 'Engine 2')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_txt7'), 'asd')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Please Fill Unit Sign Correctly', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-pen'))

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_txt7'), 'TEST1')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_sc_TC_From'), '01042021')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Format Must Be Appropriate', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_sc_TC_From'), '20210401')

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_sc_TC_To'), '03042021')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Format Must Be Appropriate', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Textboxes/input_-_sc_TC_To'), '20210403')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Edit/Buttons/i_Created Date_fa fa-save'))

WebUI.closeBrowser()

