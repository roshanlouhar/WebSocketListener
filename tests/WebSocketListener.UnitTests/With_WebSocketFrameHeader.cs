﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vtortola.WebSockets;
using vtortola.WebSockets.Rfc6455;

namespace WebSocketListener.UnitTests
{
    [TestClass]
    public class With_WebSocketFrameHeader
    {
        [TestMethod]
        public void With_WebSocketFrameHeaderFlags_Can_CreateSmallHeader()
        {
            var header = WebSocketFrameHeader.Create(101, true, false, WebSocketFrameOption.Text, new WebSocketExtensionFlags());
            Byte[] buffer = new Byte[2];
            header.ToBytes(buffer, 0);
            Assert.AreEqual(129, buffer[0]);
            Assert.AreEqual(101, buffer[1]);
        }

        [TestMethod]
        public void With_WebSocketFrameHeaderFlags_Can_CreateMediumHeader()
        {
            var header = WebSocketFrameHeader.Create(138, true, false, WebSocketFrameOption.Text, new WebSocketExtensionFlags());
            Byte[] buffer = new Byte[4];
            header.ToBytes(buffer, 0);
            Assert.AreEqual(129, buffer[0]);
            Assert.AreEqual(126, buffer[1]);
            Assert.AreEqual(0, buffer[2]);
            Assert.AreEqual(138, buffer[3]);
        }

        [TestMethod]
        public void With_WebSocketFrameHeaderFlags_Can_CreateBigHeader()
        {
            var header = WebSocketFrameHeader.Create(Int32.MaxValue, true, false, WebSocketFrameOption.Text, new WebSocketExtensionFlags());
            Byte[] buffer = new Byte[10];
            header.ToBytes(buffer, 0);
            Assert.AreEqual(129, buffer[0]);
            Assert.AreEqual(127, buffer[1]);
            Assert.AreEqual(0, buffer[2]);
            Assert.AreEqual(0, buffer[3]);
            Assert.AreEqual(0, buffer[4]);
            Assert.AreEqual(0, buffer[5]);
            Assert.AreEqual(127, buffer[6]);
            Assert.AreEqual(255, buffer[7]);
            Assert.AreEqual(255, buffer[8]);
            Assert.AreEqual(255, buffer[9]);
        }

        [TestMethod]
        public void With_WebSocketFrameHeaderFlags_Can_CreateStartPartialFrameHeader()
        {
            var header = WebSocketFrameHeader.Create(101, false, false, WebSocketFrameOption.Text, new WebSocketExtensionFlags());
            Byte[] buffer = new Byte[2];
            header.ToBytes(buffer, 0);
            Assert.AreEqual(1, buffer[0]);
            Assert.AreEqual(101, buffer[1]);
        }

        [TestMethod]
        public void With_WebSocketFrameHeaderFlags_Can_CreateContinuationPartialFrameHeader()
        {
            var header = WebSocketFrameHeader.Create(101, false, true, WebSocketFrameOption.Text, new WebSocketExtensionFlags());
            Byte[] buffer = new Byte[2];
            header.ToBytes(buffer, 0);
            Assert.AreEqual(0, buffer[0]);
            Assert.AreEqual(101, buffer[1]);
        }

        [TestMethod]
        public void With_WebSocketFrameHeaderFlags_Can_CreateFinalPartialFrameHeader()
        {
            var header = WebSocketFrameHeader.Create(101, true, true, WebSocketFrameOption.Text, new WebSocketExtensionFlags());
            Byte[] buffer = new Byte[2];
            header.ToBytes(buffer, 0);
            Assert.AreEqual(128, buffer[0]);
            Assert.AreEqual(101, buffer[1]);
        }

        [TestMethod]
        public void With_WebSocketFrameHeaderFlags_Can_CreateBinaryFrameHeader()
        {
            var header = WebSocketFrameHeader.Create(101, true, false, WebSocketFrameOption.Binary, new WebSocketExtensionFlags());
            Byte[] buffer = new Byte[2];
            header.ToBytes(buffer, 0);
            Assert.AreEqual(130, buffer[0]);
            Assert.AreEqual(101, buffer[1]);
        }

        [TestMethod]
        public void With_WebSocketFrameHeaderFlags_Can_CreateBinaryFrameHeader_WithExtensions()
        {
            var header = WebSocketFrameHeader.Create(101, true, false, WebSocketFrameOption.Binary, new WebSocketExtensionFlags() { Rsv1 = true, Rsv2 = true });
            Byte[] buffer = new Byte[2];
            header.ToBytes(buffer, 0);
            Assert.AreEqual(226, buffer[0]);
            Assert.AreEqual(101, buffer[1]);
        }
    }
}
