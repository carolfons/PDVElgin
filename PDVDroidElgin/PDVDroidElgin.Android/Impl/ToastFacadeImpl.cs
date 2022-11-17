using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PDVDroidElgin.Contracts;
using PDVDroidElgin.Droid.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(ToastFacadeImpl))]
namespace PDVDroidElgin.Droid.Impl
{
    public class ToastFacadeImpl : IToastFacade
    {
        public ToastFacadeImpl()
        {

        }

        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}