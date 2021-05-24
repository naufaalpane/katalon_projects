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

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/button_Delete'))

WebUI.verifyTextPresent('Select One or More Record to be Deleted', true)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/input_dummy_chkRow'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/button_Delete'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/button_No'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/button_Delete'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/button_Yes'))

WebUI.verifyTextPresent('Delete Success', true)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/button_dummy_btn initialBtn btn-md btn-outl_3b0b48'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/button_No'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Copy_Line/i_Created Date_fa fa-trash'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/button_Yes'))

WebUI.verifyTextPresent('Delete Success', true)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/Multiple_Checkbox/input_Created Date_chkRow'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/Multiple_Checkbox/input_dummy_chkRow'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/button_Delete'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/button_No'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/button_Delete'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/button_Yes'))

WebUI.verifyTextPresent('Delete Success', true)

WebUI.closeBrowser()

