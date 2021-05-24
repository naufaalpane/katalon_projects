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

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Add'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Created Date_cancelSubmit'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Add'))

WebUI.verifyElementNotClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Add'))

WebUI.verifyElementNotClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/button_Delete'))

WebUI.verifyElementNotClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/button_Search'))

WebUI.verifyElementNotClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Line Code _linecd'))

WebUI.verifyElementNotClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Line Code _linecd'))

WebUI.verifyElementNotClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/input_Summary HEIJUNKA Code  _hjkcd'))

WebUI.verifyElementNotClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Search/select_Choose                              _8b7d21'))

WebUI.verifyElementNotClickable(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Delete/button_dummy_btn initialBtn btn-md btn-outl_3b0b48'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt2'), 'K')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt3'), 'AKK3K1')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt4'), '2NR')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt5'), '123')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_sc_TC_From'), '20210316')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_sc_TC_To'), '20210318')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Created Date_btnSave'))

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Add'))

WebUI.verifyTextPresent('Process Save Capacity Master finish successfully', true)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt2'), 'M')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt3'), 'AKK3M1')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt4'), '4WD')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt5'), '1')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_sc_TC_From'), '20210309')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_sc_TC_To'), '20210310')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Process Save Capacity Master finish successfully', true)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Add'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt2'), 'K')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt3'), 'AKK3K1')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt4'), '1TR')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt5'), '2')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_sc_TC_From'), '20210323')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_sc_TC_To'), '20210324')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Process Save Capacity Master finish successfully', true)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Add'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt2'), 'K')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt3'), 'AKK3M1')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt4'), '2NR')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt5'), '3')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_sc_TC_From'), '20210325')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_sc_TC_To'), '20210326')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Process Save Capacity Master finish successfully', true)

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Add'))

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt2'), 'asd')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt3'), 'asd')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt4'), 'asd')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt5'), '3')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_sc_TC_From'), '20210325')

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_sc_TC_To'), '20210326')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Proccess Code must K or M', true)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt2'), 'K')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Line Code with Company Code 2000 not exist on Line Master', true)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt3'), 'AKK3M1')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Heijunka Code with Company Code 2000 not exist on Heijunkan Code Name Master', true)

WebUI.setText(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/input_Created Date_txt4'), '2NR')

WebUI.enhancedClick(findTestObject('WA0AUC04_Repo/WA0AUC04_Old_Repo/Add/button_Created Date_btnSave'))

WebUI.verifyTextPresent('Overlap found for T/C From and T/C to in records with the same Line Code and Heijunka Code', true)

WebUI.closeBrowser()

