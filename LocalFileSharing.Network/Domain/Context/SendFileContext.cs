﻿using System;
using System.IO;

using LocalFileSharing.Network.Domain.States;

namespace LocalFileSharing.Network.Domain.Context {
    public class SendFileContext : FileTransferContextBase {
        public SendFileState State { get; protected set; }

        public BinaryReader Reader { get; protected set; }

        public SendFileContext(Guid transferID, SendFileState state, BinaryReader reader)
            : base(transferID) {
            if (state == SendFileState.Unspecified) {
                throw new ArgumentException(
                    $"The state value can not be unspecified.",
                    nameof(state)
                );
            }

            if (reader is null) {
                throw new ArgumentNullException(nameof(reader));
            }

            State = state;
            Reader = reader;
        }

        public override void Close() {
            Reader.Close();
        }
    }
}
