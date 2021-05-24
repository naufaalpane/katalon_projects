import static com.kms.katalon.core.checkpoint.CheckpointFactory.findCheckpoint
import static com.kms.katalon.core.testcase.TestCaseFactory.findTestCase
import static com.kms.katalon.core.testdata.TestDataFactory.findTestData
import static com.kms.katalon.core.testobject.ObjectRepository.findTestObject
import static com.kms.katalon.core.testobject.ObjectRepository.findWindowsObject
import java.text.DateFormat as DateFormat
import java.text.SimpleDateFormat as SimpleDateFormat
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

WebUI.delay(5)

arg1 = WebUI.getText(findTestObject('WA0AUC02_Repo/Format_Checking_Repo/Date/td_20210401'))

println(arg1)

DateFormat df1 = new SimpleDateFormat('yyyy/MM/dd')

Date date1 = df1.parse(arg1)

SimpleDateFormat sdf1 = new SimpleDateFormat('yyyy/MM/dd')

String finals1 = sdf1.format(date1)

println('' + finals1)

arg2 = '2021/04/01'

WebUI.verifyEqual(arg1, arg2)

arg3 = WebUI.getText(findTestObject('WA0AUC02_Repo/Format_Checking_Repo/Date/td_20210403'))

println(arg3)

DateFormat df2 = new SimpleDateFormat('yyyy/MM/dd')

Date date2 = df2.parse(arg3)

SimpleDateFormat sdf2 = new SimpleDateFormat('yyyy/MM/dd')

String finals2 = sdf2.format(date2)

println('' + finals2)

arg4 = '2021/04/03'

WebUI.verifyEqual(arg3, arg4)

WebUI.closeBrowser()

