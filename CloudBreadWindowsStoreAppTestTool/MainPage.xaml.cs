using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


//추가
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Security.Cryptography.DataProtection;


// 빈 페이지 항목 템플릿에 대한 설명은 http://go.microsoft.com/fwlink/?LinkId=234238에 나와 있습니다.

namespace CloudBreadWindowsStoreAppTestTool
{
    /// <summary>
    /// 자체에서 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            //this.serverURL.Text = "https://cloudbread0330.azure-mobile.net/";
            this.serverURL.Text = "http://dw-cloudbread2.azurewebsites.net/";
            //this.serverURL.Text = "http://localhost:1477";

            this.serverKey.Password = "";       // 서버키
            
        }

        public void ClearTextbox()
        {
            outputTextbox.Text = "";
            inputTextbox.Text = "";
        }

        public class SelLoginIDDupeCheck { public string memberID;}

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearTextbox();
            
            try
            {
                SelLoginIDDupeCheck selLoginIDDupeCheck = new SelLoginIDDupeCheck();
                //selLoginIDDupeCheck.memberID = crypto
                
                var client = new MobileServiceClient(serverURL.Text);
                string j = @"{""memberID"": ""aaa"" }";

                //토큰 = body 텍스트 - 으로 바로 로드
                inputTextbox.Text = j;
                JToken token = JObject.Parse(j);

                var orderResult = await client.InvokeApiAsync("CBSelLoginIDDupeCheck", token);
                //var orderResult = await client.InvokeApiAsync("values");
                outputTextbox.Text = orderResult.ToString();

            }
                catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private async void CBInsRegMemberClick(object sender, RoutedEventArgs e)
        {
            ClearTextbox();

            try
            {
                var client = new MobileServiceClient(serverURL.Text);
                string j = @"{
                      ""memberID_Members"": ""회원ID-GUID"",
                      ""memberPWD_Members"": ""GUID"",
                      ""emailAddress_Members"": ""GUID@GUID.com"",
                      ""emailConfirmedYN_Members"": ""N"",			
                      ""phoneNumber1_Members"": ""전화번호1-GUID"",
                      ""phoneNumber2_Members"": ""전화번호2-GUID"",
                      ""piNumber_Members"": ""개인식별번호-GUID"",
                      ""name1_Members"": ""이름1-GUID"",
                      ""name2_Members"": ""이름2-GUID"",
                      ""name3_Members"": ""이름3-GUID"",
                      ""dob_Members"": ""19990101"",
                      ""recommenderID_Members"": ""추천인ID-GUID"",
                      ""memberGroup_Members"": ""회원그룹-GUID"",
                      ""lastDeviceID_Members"": ""최종접속시디바이스ID-GUID"",
                      ""lastIPaddress_Members"": ""최종접속IP주소-GUID"",
                      ""lastLoginDT_Members"": ""1900-01-01"",
                      ""lastLogoutDT_Members"": ""1900-01-01"",
                      ""lastMACAddress_Members"": ""최종접속MAC주소-GUID"",
                      ""accountBlockYN_Members"": ""N"",
                      ""accountBlockEndDT_Members"": ""1900-01-01"",
                      ""anonymousYN_Members"": ""N"",
                      ""_3rdAuthProvider_Members"": ""외부인증제공자"",
                      ""_3rdAuthID_Members"": ""외부인증ID"",
                      ""_3rdAuthParam_Members"": ""외부인증파라미터"",
                      ""pushNotificationID_Members"": ""푸쉬ID"",
                      ""pushNotificationProvider_Members"": ""푸쉬서비스제공자"",
                      ""pushNotificationGroup_Members"": ""푸쉬그룹"",
                      ""sCol1_Members"": ""여분의컬럼1-GUID"",
                      ""sCol2_Members"": ""여분의컬럼2-GUID"",
                      ""sCol3_Members"": ""여분의컬럼3-GUID"",
                      ""sCol4_Members"": ""여분의컬럼4-GUID"",
                      ""sCol5_Members"": ""여분의컬럼5-GUID"",
                      ""sCol6_Members"": ""여분의컬럼6-GUID"",
                      ""sCol7_Members"": ""여분의컬럼7-GUID"",
                      ""sCol8_Members"": ""여분의컬럼8-GUID"",
                      ""sCol9_Members"": ""여분의컬럼9-GUID"",
                      ""sCol10_Members"": ""여분의컬럼10-GUID"",
                      ""timeZoneID_Members"": ""Korea Standard Time"",
                      ""level_MemberGameInfoes"": ""1"",
                      ""exps_MemberGameInfoes"": ""0"",
                      ""points_MemberGameInfoes"": ""1000"",
                      ""userSTAT1_MemberGameInfoes"": ""사용자상태속성1-GUID"",
                      ""userSTAT2_MemberGameInfoes"": ""사용자상태속성2-GUID"",
                      ""userSTAT3_MemberGameInfoes"": ""사용자상태속성3-GUID"",
                      ""userSTAT4_MemberGameInfoes"": ""사용자상태속성5-GUID"",
                      ""userSTAT5_MemberGameInfoes"": ""사용자상태속성5-GUID"",
                      ""userSTAT6_MemberGameInfoes"": ""사용자상태속성6-GUID"",
                      ""userSTAT7_MemberGameInfoes"": ""사용자상태속성7-GUID"",
                      ""userSTAT8_MemberGameInfoes"": ""사용자상태속성8-GUID"",
                      ""userSTAT9_MemberGameInfoes"": ""사용자상태속성9-GUID"",
                      ""userSTAT10_MemberGameInfoes"": ""사용자상태속성10-GUID"",
                      ""sCol1_MemberGameInfoes"": ""여분의컬럼1-GUID"",
                      ""sCol2_MemberGameInfoes"": ""여분의컬럼2-GUID"",
                      ""sCol3_MemberGameInfoes"": ""여분의컬럼3-GUID"",
                      ""sCol4_MemberGameInfoes"": ""여분의컬럼4-GUID"",
                      ""sCol5_MemberGameInfoes"": ""여분의컬럼5-GUID"",
                      ""sCol6_MemberGameInfoes"": ""여분의컬럼6-GUID"",
                      ""sCol7_MemberGameInfoes"": ""여분의컬럼7-GUID"",
                      ""sCol8_MemberGameInfoes"": ""여분의컬럼8-GUID"",
                      ""sCol9_MemberGameInfoes"": ""여분의컬럼9-GUID"",
                      ""sCol10_MemberGameInfoes"": ""여분의컬럼10-GUID""
                    }";

                //토큰 = body 텍스트 - 으로 바로 로드
                inputTextbox.Text = j;
                JToken token = JObject.Parse(j);

                var orderResult = await client.InvokeApiAsync("CBInsRegMember", token);
                outputTextbox.Text = orderResult.ToString();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void CBSelMemberItemsClick(object sender, RoutedEventArgs e)
        {
            ClearTextbox();

            try
            {
                var client = new MobileServiceClient(serverURL.Text);
                string j = @"{
                              ""memberID"": ""aaa"",
                                ""page"": 1,
                              ""pageSize"": 5
                            }";

                //토큰 = body 텍스트 - 으로 바로 로드
                inputTextbox.Text = j;
                JToken token = JObject.Parse(j);

                var orderResult = await client.InvokeApiAsync("CBSelMemberItems", token);
                outputTextbox.Text = orderResult.ToString();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void CBSelNoticesClick(object sender, RoutedEventArgs e)
        {
            ClearTextbox();

            try
            {
                var client = new MobileServiceClient(serverURL.Text);

                string j = @"{
                          ""memberID"": ""aaa""
                        }";

                //토큰 = body 텍스트 - 으로 바로 로드
                inputTextbox.Text = j;
                JToken token = JObject.Parse(j);

                var orderResult = await client.InvokeApiAsync("CBSelNotices", token);        //null 로 전달
                outputTextbox.Text = orderResult.ToString();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void CBSelGameEventsClick(object sender, RoutedEventArgs e)
        {
            ClearTextbox();

            try
            {
                var client = new MobileServiceClient(serverURL.Text);
                string j = @"{
                          ""memberID"": ""aaa""
                        }";

                //토큰 = body 텍스트 - 으로 바로 로드
                inputTextbox.Text = j;
                JToken token = JObject.Parse(j);

                var orderResult = await client.InvokeApiAsync("CBSelGameEvents", token);
                outputTextbox.Text = orderResult.ToString();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void CBSelCouponsClick(object sender, RoutedEventArgs e)
        {
            ClearTextbox();

            try
            {
                var client = new MobileServiceClient(serverURL.Text);
                string j = @"{
                  ""memberID"": ""aaa""
                }";

                //토큰 = body 텍스트 - 으로 바로 로드
                inputTextbox.Text = j;
                JToken token = JObject.Parse(j);

                var orderResult = await client.InvokeApiAsync("CBSelCoupons", token);
                outputTextbox.Text = orderResult.ToString();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void CBAddUseMemberItemINSERTClick(object sender, RoutedEventArgs e)
        {
            ClearTextbox();

            try
            {
                var client = new MobileServiceClient(serverURL.Text);
                string j = @"{
                      ""insertORUpdateORDelete"": ""INSERT"",
                      ""memberItemID_MemberItem"": ""GUID"",
                      ""memberID_MemberItem"": ""aaa"",
                      ""itemListID_MemberItem"": ""아이템4"",
                      ""itemCount_MemberItem"": ""10"",
                      ""itemStatus_MemberItem"": ""상태좋음"",
                      ""hideYN_MemberItem"": ""N"",
                      ""deleteYN_MemberItem"": ""N"",
                      ""sCol1_MemberItem"": ""여분의컬럼1-GUID"",
                      ""sCol2_MemberItem"": ""여분의컬럼2-GUID"",
                      ""sCol3_MemberItem"": ""여분의컬럼3-GUID"",
                      ""sCol4_MemberItem"": ""여분의컬럼4-GUID"",
                      ""sCol5_MemberItem"": ""여분의컬럼5-GUID"",
                      ""sCol6_MemberItem"": ""여분의컬럼6-GUID"",
                      ""sCol7_MemberItem"": ""여분의컬럼7-GUID"",
                      ""sCol8_MemberItem"": ""여분의컬럼8-GUID"",
                      ""sCol9_MemberItem"": ""여분의컬럼9-GUID"",
                      ""sCol10_MemberItem"": ""여분의컬럼10-GUID"",
                      ""memberID_MemberGameInfoes"": ""aaa"",
                      ""level_MemberGameInfoes"": ""2"",
                      ""exps_MemberGameInfoes"": ""20"",
                      ""points_MemberGameInfoes"": ""20"",
                      ""userSTAT1_MemberGameInfoes"": ""게임중변경된상태값1-GUID"",
                      ""userSTAT2_MemberGameInfoes"": ""게임중변경된상태값2-GUID"",
                      ""userSTAT3_MemberGameInfoes"": ""게임중변경된상태값3-GUID"",
                      ""userSTAT4_MemberGameInfoes"": ""게임중변경된상태값4-GUID"",
                      ""userSTAT5_MemberGameInfoes"": ""게임중변경된상태값5-GUID"",
                      ""userSTAT6_MemberGameInfoes"": ""게임중변경된상태값6-GUID"",
                      ""userSTAT7_MemberGameInfoes"": ""게임중변경된상태값7-GUID"",
                      ""userSTAT8_MemberGameInfoes"": ""게임중변경된상태값8-GUID"",
                      ""userSTAT9_MemberGameInfoes"": ""게임중변경된상태값9-GUID"",
                      ""userSTAT10_MemberGameInfoes"": ""게임중변경된상태값10-GUID"",
                      ""sCol1_MemberGameInfoes"": ""게임중변경된여분의컬럼1-GUID"",
                      ""sCol2_MemberGameInfoes"": ""게임중변경된여분의컬럼2-GUID"",
                      ""sCol3_MemberGameInfoes"": ""게임중변경된여분의컬럼3-GUID"",
                      ""sCol4_MemberGameInfoes"": ""게임중변경된여분의컬럼4-GUID"",
                      ""sCol5_MemberGameInfoes"": ""게임중변경된여분의컬럼5-GUID"",
                      ""sCol6_MemberGameInfoes"": ""게임중변경된여분의컬럼6-GUID"",
                      ""sCol7_MemberGameInfoes"": ""게임중변경된여분의컬럼7-GUID"",
                      ""sCol8_MemberGameInfoes"": ""게임중변경된여분의컬럼8-GUID"",
                      ""sCol9_MemberGameInfoes"": ""게임중변경된여분의컬럼9-GUID"",
                      ""sCol10_MemberGameInfoes"": ""게임중변경된여분의컬럼10-GUID""
                    }";

                //토큰 = body 텍스트 - 으로 바로 로드
                inputTextbox.Text = j;
                JToken token = JObject.Parse(j);
                var orderResult = await client.InvokeApiAsync("CBAddUseMemberItem", token);
                outputTextbox.Text = orderResult.ToString();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


        private async void CBAddUseMemberItemUPDATEClick(object sender, RoutedEventArgs e)
        {
            ClearTextbox();

            try
            {
                var client = new MobileServiceClient(serverURL.Text);
                string j = @"{
                          ""insertORUpdateORDelete"": ""UPDATE"",
                          ""memberItemID_MemberItem"": ""회원의아이템ID1-원래GUID값"",
                          ""memberID_MemberItem"": ""aaa"",
                          ""itemListID_MemberItem"": ""아이템4"",
                          ""itemCount_MemberItem"": ""999"",
                          ""itemStatus_MemberItem"": ""상태나쁨"",
                          ""hideYN_MemberItem"": ""N"",
                          ""deleteYN_MemberItem"": ""N"",
                          ""sCol1_MemberItem"": ""변경된여분의컬럼1-GUID"",
                          ""sCol2_MemberItem"": ""변경된여분의컬럼2-GUID"",
                          ""sCol3_MemberItem"": ""변경된여분의컬럼3-GUID"",
                          ""sCol4_MemberItem"": ""변경된여분의컬럼4-GUID"",
                          ""sCol5_MemberItem"": ""변경된여분의컬럼5-GUID"",
                          ""sCol6_MemberItem"": ""변경된여분의컬럼6-GUID"",
                          ""sCol7_MemberItem"": ""변경된여분의컬럼7-GUID"",
                          ""sCol8_MemberItem"": ""변경된여분의컬럼8-GUID"",
                          ""sCol9_MemberItem"": ""변경된여분의컬럼9-GUID"",
                          ""sCol10_MemberItem"": ""변경된여분의컬럼10-GUID"",
                          ""memberID_MemberGameInfoes"": ""aaa"",
                          ""level_MemberGameInfoes"": ""22"",
                          ""exps_MemberGameInfoes"": ""222"",
                          ""points_MemberGameInfoes"": ""222"",
                          ""userSTAT1_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된상태값1-GUID"",
                          ""userSTAT2_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된상태값2-GUID"",
                          ""userSTAT3_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된상태값3-GUID"",
                          ""userSTAT4_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된상태값4-GUID"",
                          ""userSTAT5_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된상태값5-GUID"",
                          ""userSTAT6_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된상태값6-GUID"",
                          ""userSTAT7_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된상태값7-GUID"",
                          ""userSTAT8_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된상태값8-GUID"",
                          ""userSTAT9_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된상태값9-GUID"",
                          ""userSTAT10_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된상태값10-GUID"",
                          ""sCol1_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된여분의컬럼1-GUID"",
                          ""sCol2_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된여분의컬럼2-GUID"",
                          ""sCol3_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된여분의컬럼3-GUID"",
                          ""sCol4_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된여분의컬럼4-GUID"",
                          ""sCol5_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된여분의컬럼5-GUID"",
                          ""sCol6_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된여분의컬럼6-GUID"",
                          ""sCol7_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된여분의컬럼7-GUID"",
                          ""sCol8_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된여분의컬럼8-GUID"",
                          ""sCol9_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된여분의컬럼9-GUID"",
                          ""sCol10_MemberGameInfoes"": ""다시UPDATE로변경된게임중변경된여분의컬럼10-GUID""
                        }";

                //토큰 = body 텍스트 - 으로 바로 로드
                inputTextbox.Text = j;
                JToken token = JObject.Parse(j);
                var orderResult = await client.InvokeApiAsync("CBAddUseMemberItem", token);
                outputTextbox.Text = orderResult.ToString();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void CBAddUseMemberItemDELETEClick(object sender, RoutedEventArgs e)
        {
            ClearTextbox();

            try
            {
                var client = new MobileServiceClient(serverURL.Text);
                string j = @"{
                      ""insertORUpdateORDelete"": ""DELETE"",
                      ""memberItemID_MemberItem"": ""회원의아이템ID1-원래고유값"",
                      ""memberID_MemberItem"": null,
                      ""itemListID_MemberItem"": null,
                      ""itemCount_MemberItem"": null,
                      ""itemStatus_MemberItem"": null,
                      ""hideYN_MemberItem"": null,
                      ""deleteYN_MemberItem"": null,
                      ""sCol1_MemberItem"": null,
                      ""sCol2_MemberItem"": null,
                      ""sCol3_MemberItem"": null,
                      ""sCol4_MemberItem"": null,
                      ""sCol5_MemberItem"": null,
                      ""sCol6_MemberItem"": null,
                      ""sCol7_MemberItem"": null,
                      ""sCol8_MemberItem"": null,
                      ""sCol9_MemberItem"": null,
                      ""sCol10_MemberItem"": null,
                      ""memberID_MemberGameInfoes"": ""aaa"",
                      ""level_MemberGameInfoes"": ""99"",
                      ""exps_MemberGameInfoes"": ""999"",
                      ""points_MemberGameInfoes"": ""999"",
                      ""userSTAT1_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된상태값1-GUID"",
                      ""userSTAT2_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된상태값2-GUID"",
                      ""userSTAT3_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된상태값3-GUID"",
                      ""userSTAT4_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된상태값4-GUID"",
                      ""userSTAT5_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된상태값5-GUID"",
                      ""userSTAT6_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된상태값6-GUID"",
                      ""userSTAT7_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된상태값7-GUID"",
                      ""userSTAT8_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된상태값8-GUID"",
                      ""userSTAT9_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된상태값9-GUID"",
                      ""userSTAT10_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된상태값10-GUID"",
                      ""sCol1_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된여분의컬럼1-GUID"",
                      ""sCol2_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된여분의컬럼2-GUID"",
                      ""sCol3_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된여분의컬럼3-GUID"",
                      ""sCol4_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된여분의컬럼4-GUID"",
                      ""sCol5_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된여분의컬럼5-GUID"",
                      ""sCol6_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된여분의컬럼6-GUID"",
                      ""sCol7_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된여분의컬럼7-GUID"",
                      ""sCol8_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된여분의컬럼8-GUID"",
                      ""sCol9_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된여분의컬럼9-GUID"",
                      ""sCol10_MemberGameInfoes"": ""다시DELETE로변경된게임중변경된여분의컬럼10-GUID""
                    }";
                        // null 으로 주거나 로우를 뺀다.

                //토큰 = body 텍스트 - 으로 바로 로드
                inputTextbox.Text = j;
                JToken token = JObject.Parse(j);
                var orderResult = await client.InvokeApiAsync("CBAddUseMemberItem", token);
                outputTextbox.Text = orderResult.ToString();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}
