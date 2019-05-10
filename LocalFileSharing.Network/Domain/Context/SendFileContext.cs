﻿using System.IO;

namespace LocalFileSharing.Network.Domain.Context {
    public class SendFileContext : FileTransferContextBase {
        public long BytesSent { get; set; }
        public BinaryReader Reader { get; set; }

        public override void End() {
            Reader.Close();
        }

        public override void Cancel() {
            base.Cancel();
            Reader.Close();
        }
    }
}
