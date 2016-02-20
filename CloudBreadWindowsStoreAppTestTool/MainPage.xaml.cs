/**
* @file MainPage.xaml.cs
* @brief Click generate button to login on auth provider and generate authkey. For more information, visit CloudBread project website API test guide. \n
* generate 버튼을 클릭해 인증 제공자 서비스에 로그인 하면 authkey가 발급됨. 추가적인 정보는 CloudBread 공식 웹사이트 가이드 참조.
* @author Dae Woo Kim
*/

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

using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;


namespace CloudBreadWindowsStoreAppTestTool
{
    public sealed partial class MainPage : Page
    {
        private MobileServiceUser user;

        private async System.Threading.Tasks.Task<bool> AuthenticateAsync()
        {
            string message;
            bool success = false;
            try
            {
                /**
                * @brief Change MobileServiceClient object url as yours \n
                * Sign-in using Facebook authentication. \n
                * After setup on Azure Moilbe App portal authentication, change property for twitter, google id, microsoft id \n
                * 기본 페이스북 인증을 사용. \n
                * 다른 인증을 이용할 경우 Azure 포털 Mobile App의 인증 정보를 설정하고 twitter, google id, microsoft id 속성으로 변경.
                */
                user = await App.MobileService
                    .LoginAsync(MobileServiceAuthenticationProvider.Facebook);
                txtSID.Text = user.UserId;
                txtAuthKey.Text = user.MobileServiceAuthenticationToken;

                success = true;
            }
            catch (InvalidOperationException)
            {
                message = "Generate auth key. You must log in";
            }

            return success;
        }

        public MainPage()
        {
            this.InitializeComponent();
        }

        /** @brief Genate authkey
        *   인증키 생성
        */
        private async void btnGen_Click(object sender, RoutedEventArgs e)
        {
            await AuthenticateAsync();
        }
    }
}
