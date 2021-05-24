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

WebUI.selectOptionByValue(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/select_3                5                10_e5027c'), '3',
    true)

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_AKK3K1'), 'AKK3K1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_AKK3K1_2'), 'AKK3K1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_AKK3K1_3'), 'AKK3K1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_1TR'), '1TR')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_2NR'), '2NR')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_4WD'), '4WD')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_2,000000'), '2,000000')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_123,000000'), '123,000000')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_4,000000'), '4,000000')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/a_2'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_AKK3M1'), 'AKK3M1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_AKK3M1_2'), 'AKK3M1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_AKK3M2'), 'AKK3M2')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_2NR'), '2NR')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_4WD'), '4WD')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_1TR'), '1TR')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_3,000000'), '3,000000')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_1,000000'), '1,000000')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_6,000000'), '6,000000')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/a_Prev'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_AKK3K1'), 'AKK3K1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_AKK3K1_2'), 'AKK3K1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_AKK3K1_3'), 'AKK3K1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_1TR'), '1TR')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_2NR'), '2NR')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_4WD'), '4WD')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_2,000000'), '2,000000')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_123,000000'), '123,000000')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_4,000000'), '4,000000')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/a_Next'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_AKK3M1'), 'AKK3M1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_AKK3M1_2'), 'AKK3M1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_AKK3M2'), 'AKK3M2')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_2NR'), '2NR')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_4WD'), '4WD')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_1TR'), '1TR')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_3,000000'), '3,000000')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_1,000000'), '1,000000')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_6,000000'), '6,000000')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/a_First'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_AKK3K1'), 'AKK3K1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_AKK3K1_2'), 'AKK3K1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_AKK3K1_3'), 'AKK3K1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_1TR'), '1TR')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_2NR'), '2NR')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_4WD'), '4WD')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_2,000000'), '2,000000')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_123,000000'), '123,000000')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_1/td_4,000000'), '4,000000')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/a_Last'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_3/td_AKK3M3'), 'AKK3M3')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_3/td_AKK3M4'), 'AKK3M4')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_3/td_AT'), 'AT')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_3/td_2NR'), '2NR')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_3/td_10,000000'), '10,000000')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_3/td_80,000000'), '80,000000')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/input_Last_gotovalue'), '2')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/button_Go'))

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_AKK3M1'), 'AKK3M1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_AKK3M1_2'), 'AKK3M1')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_AKK3M2'), 'AKK3M2')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_2NR'), '2NR')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_4WD'), '4WD')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_1TR'), '1TR')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_3,000000'), '3,000000')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_1,000000'), '1,000000')

WebUI.verifyElementText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Page_2/td_6,000000'), '6,000000')

WebUI.selectOptionByValue(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/select_3                5                10_e5027c'), '10',
    true)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Table_Checkboxes/input_checkall'))

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Table_Checkboxes/input_chkRow_1'), 30, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Table_Checkboxes/input_chkRow_2'), 30, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Table_Checkboxes/input_chkRow_3'), 30, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Table_Checkboxes/input_chkRow_4'), 30, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Table_Checkboxes/input_chkRow_5'), 30, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Table_Checkboxes/input_chkRow_6'), 30, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Table_Checkboxes/input_chkRow_7'), 30, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.verifyElementChecked(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Table/Table_Checkboxes/input_chkRow_8'), 30, FailureHandling.CONTINUE_ON_FAILURE)

WebUI.takeScreenshot()

WebUI.closeBrowser()

