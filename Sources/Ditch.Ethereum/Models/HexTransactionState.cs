﻿using System;
using Ditch.Core.Converters;
using Newtonsoft.Json;

namespace Ditch.Ethereum.Models
{
    [JsonConverter(typeof(CustomJsonConverter))]
    public class HexTransactionStatus : HexValue
    {
        private const int MinCount = 0;
        private const int MaxCount = 1;

        protected override bool PrintZero => false;

        private TransactionStatus? _value;
        public TransactionStatus Value
        {
            get
            {
                if (!_value.HasValue)
                    _value = ToTransactionStatus();

                return _value.Value;
            }
        }


        public HexTransactionStatus()
        {
            MinBytes = MinCount;
            MaxBytes = MaxCount;
        }

        public HexTransactionStatus(string value)
            : base(value, MinCount, MaxCount) { }

        public HexTransactionStatus(byte[] value)
            : base(value, MinCount, MaxCount) { }

        public HexTransactionStatus(byte[] source, int start, int count, bool trimZero = true)
            : base(source, start, count, MinCount, MaxCount, trimZero) { }


        private TransactionStatus ToTransactionStatus()
        {
            if (IsNull)
                return TransactionStatus.Unknown;

            switch (Bytes[0])
            {
                case 0:
                    return TransactionStatus.Failed;
                case 1:
                    return TransactionStatus.Successful;
            }

            throw new NotImplementedException($"ToTransactionStatus = {Bytes[0]}");
        }


        public override string ToString()
        {
            return $"{Value}";
        }

        public enum TransactionStatus
        {
            Unknown = 0,

            Failed = 1,
            Successful = 2,
            Null = 3
        }
    }
}