﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows;
using SchoolLib.Proxies;
//using SchoolLib.Contracts;
using SchoolLib.Client.Contracts;
using System.Threading;
using System.Diagnostics;
using SchoolLib.Contracts;

namespace SchoolLib.Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId +
                " | Process " + Process.GetCurrentProcess().Id.ToString();
        }

        private void btnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            StudentClient proxy = new StudentClient("webEP");
            var students = proxy.GetStudents();

            proxy.Close();
            
        }

        private void btnGetZipCodes_Click(object sender, RoutedEventArgs e)
        {
            if (txtState.Text != null)
            {
                //EndpointAddress address = new EndpointAddress("net.tcp://localhost:8009/GeoService");
                //Binding binding = new NetTcpBinding();

                //GeoClient proxy = new GeoClient(binding, address);
                //IEnumerable<ZipCodeData> data = proxy.GetZips(txtState.Text);
                //if (data != null)
                //    lstZips.ItemsSource = data;

                //proxy.Close();
            }
        }

        private void btnMakeCall_Click(object sender, RoutedEventArgs e)
        {
            EndpointAddress address = new EndpointAddress("net.tcp://localhost:8010/MessageService");
            Binding binding = new NetTcpBinding();

            ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>(binding, address);

            //ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");
            IMessageService proxy = factory.CreateChannel();

            proxy.ShowMessage(txtMessage.Text);

            factory.Close();
        }
    }
}
