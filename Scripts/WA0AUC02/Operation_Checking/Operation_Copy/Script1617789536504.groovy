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

WebUI.enhancedClick(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/a_Master'))

WebUI.enhancedClick(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/a_WA0AUC02'))

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Copy/i_Created Date_fa fa-copy'))

WebUI.enhancedClick(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/i_Created Date_fa fa-times'))

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Copy/i_Created Date_fa fa-copy'))

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Copy/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('ERROR: Data is already exists', true)

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_Created Date_txt2'), '')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Copy/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Car Family Code should not be empty', true)

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_Created Date_txt2'), '272W')

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_Created Date_partsno1'), 
    '')

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_partsno12'), '')

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_partsno13'), '')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Copy/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('First Part No. should not be empty', true)

arg1 = WebUI.getAttribute(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_Created Date_partsno1'), 
    'value')

arg2 = WebUI.getAttribute(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_partsno12'), 
    'value')

arg3 = WebUI.getAttribute(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_partsno13'), 
    'value')

arg4 = '12000'

arg5 = '0C020'

arg6 = '00'

WebUI.verifyEqual(arg1, arg4, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyEqual(arg2, arg5, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyEqual(arg3, arg6, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_Created Date_partsno1'), 
    '12000')

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_partsno12'), '0C020')

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_partsno13'), '00')

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_txt4'), '')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Copy/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Line Code should not be empty', true)

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_txt4'), 'AKS2K1')

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_txt5'), '')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Copy/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Status Code should not be empty', true)

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_txt5'), '1')

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_txt6'), '')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Copy/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Parts Name should not be empty', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Copy/i_Created Date_fa fa-copy'))

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_txt6'), 'Engine 2')

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_txt7'), '')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Copy/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Unit Sign should not be empty', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_txt7'), 'TEST1')

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_Created Date_partsno1'), 
    '12000')

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_partsno12'), '0C360')

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_partsno13'), '00')

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_sc_TC_From'), '')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Copy/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('TC From should not be empty', true)

WebUI.setText(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_sc_TC_From'), '20210401')

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_sc_TC_To'), '')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Copy/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('TC To should not be empty', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.setText(findTestObject('Object Repository/WA0AUC02_Repo/Operation_Checking_Repo/Copy/input_-_sc_TC_To'), '20210403')

WebUI.enhancedClick(findTestObject('WA0AUC02_Repo/Operation_Checking_Repo/Copy/i_Created Date_fa fa-save'))

WebUI.verifyTextPresent('Process Save Unit Production Control Master finish successfully', true, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.closeBrowser()

