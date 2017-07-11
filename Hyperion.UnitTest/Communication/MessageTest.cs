﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hyperion.UnitTest
{
    using Poseidon.Common;
    using Hyperion.ControlClient.Communication;
    using Hyperion.ControlClient.Protocol;

    /// <summary>
    /// 报文测试
    /// </summary>
    [TestClass]
    public class MessageTest
    {
        #region Constructor
        public MessageTest()
        {
            string host = "http://192.168.0.116:8080/APP";
            Cache.Instance.Add("EquipmentHost", host);
        }
        #endregion //Constructor

        #region Test
        /// <summary>
        /// 组合报文测试
        /// </summary>
        [TestMethod]
        public void MakeMessageTest()
        {
            string userId = "gh-qc-ios";//Guid.NewGuid().ToString();
            string serialNumber = "G123456789";

            WebControlMessage message = new WebControlMessage(userId, serialNumber);

            var msg = message.GetMessage();

            Console.WriteLine(msg);

            Assert.AreEqual("Homeconsole01.00000000010030004F00060001200340001400010009gh-qc-ios0127000AG123456789001A0001100120009000100010", msg);
        }

        /// <summary>
        /// 注册测试
        /// </summary>
        [TestMethod]
        public void TestRegistration()
        {
            RegistrationMessage message = new RegistrationMessage(1, "admintest", 2334, 0, "1357924680");

            var msg = message.GetMessage();


            var task = Task.Run(() =>
            {
                Request request = new Request();
                var data = request.Post(msg);

                return data;
            });

            var result = task.Result;
            var response = result.Content.ReadAsStringAsync().Result;

            Console.WriteLine($"response message: {response}");

            RegistrationAckMessage ack = new RegistrationAckMessage();
            ack.ParseAck(response);

            Console.WriteLine($"ack result: {TLVCode.ServerReturnCode[ack.ServerResult.Value]}");

            Assert.AreEqual("F", ack.ServerResult.Value);
        }
        #endregion //Test
    }
}