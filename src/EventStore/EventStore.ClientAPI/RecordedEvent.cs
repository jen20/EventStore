﻿using System;
using System.Text;
using EventStore.ClientAPI.Messages;

namespace EventStore.ClientAPI
{
    /// <summary>
    /// Represents a previously written event
    /// </summary>
    public class RecordedEvent
    {
        /// <summary>
        /// The Event Stream that this event belongs to
        /// </summary>
        public readonly string EventStreamId;

        /// <summary>
        /// The Unique Identifier representing this event
        /// </summary>
        public readonly Guid EventId;

        /// <summary>
        /// The number of this event in the stream
        /// </summary>
        public readonly int EventNumber;

        /// <summary>
        /// The type of event this is
        /// </summary>
        public readonly string EventType;

        /// <summary>
        /// A byte array representing the data of this event
        /// </summary>
        public readonly byte[] Data;

        /// <summary>
        /// A byte array representing the metadata associated with this event
        /// </summary>
        public readonly byte[] Metadata;

        /// <summary>
        /// A datetime representing when this event was created in the system
        /// </summary>
        public DateTime Created;

#if DEBUG
        public string DebugDataView
        {
            get { return Encoding.UTF8.GetString(Data); }
        }

        public string DebugMetadataView
        {
            get { return Encoding.UTF8.GetString(Metadata); }
        }
#endif


        internal RecordedEvent(ClientMessage.EventRecord systemRecord)
        {
            EventStreamId = systemRecord.EventStreamId;

            EventId = new Guid(systemRecord.EventId);
            EventNumber = systemRecord.EventNumber;

            EventType = systemRecord.EventType;
            if (systemRecord.Created.HasValue)
                Created = DateTime.FromBinary(systemRecord.Created.Value);
            Data = systemRecord.Data ?? Empty.ByteArray;
            Metadata = systemRecord.Metadata ?? Empty.ByteArray;
        }
    }
}
