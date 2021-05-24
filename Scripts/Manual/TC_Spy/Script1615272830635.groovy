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

WebUI.navigateToUrl('https://www.thetestingworld.com/testings/')

if (myInput == 11) {
    WebUI.setText(findTestObject('TC_Spy_Result/Page_Login  Sign Up Forms/input_Address type HomeOffice_fld_username'), 
        'bebekebo')

    WebUI.setText(findTestObject('TC_Spy_Result/Page_Login  Sign Up Forms/input_Address type HomeOffice_fld_email'), 'abc@gmail.com')

    WebUI.setEncryptedText(findTestObject('TC_Spy_Result/Page_Login  Sign Up Forms/input_Address type HomeOffice_fld_password'), 
        '3thQyGz/11uuKzMsrV3OGg==')

    WebUI.setEncryptedText(findTestObject('TC_Spy_Result/Page_Login  Sign Up Forms/input_Address type HomeOffice_fld_cpassword'), 
        '3thQyGz/11uuKzMsrV3OGg==')
} else {
    WebUI.click(findTestObject('TC_Spy_Result/Page_Login  Sign Up Forms/input_Address type HomeOffice_dob'))

    WebUI.setText(findTestObject('TC_Spy_Result/Page_Login  Sign Up Forms/input_Address type HomeOffice_phone'), findTestData('TC_Data').getValue('Phone', 1))

    WebUI.setText(findTestObject('TC_Spy_Result/Page_Login  Sign Up Forms/input_Address type HomeOffice_address'), 'Jakarta')

    WebUI.click(findTestObject('TC_Spy_Result/Page_Login  Sign Up Forms/input_Address type HomeOffice_add_type'))

    WebUI.selectOptionByValue(findTestObject('TC_Spy_Result/Page_Login  Sign Up Forms/select_Choose Gender                  Male                  Female'), 
        '1', true)
}

WebUI.closeBrowser()

