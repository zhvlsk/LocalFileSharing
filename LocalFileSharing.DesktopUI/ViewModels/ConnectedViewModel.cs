﻿using System;
using System.Diagnostics;
using Caliburn.Micro;
using LocalFileSharing.Network.Domain;
using LocalFileSharing.Network.Domain.Progress;

namespace LocalFileSharing.DesktopUI.ViewModels {
    public class ConnectedViewModel : Screen {
        private bool _sending;
        private bool _receiving;
        private long _download;
        private long _upload;

        private readonly Progress<SendFileProgressReport> sendProgress;
        private readonly Progress<ReceiveFileProgressReport> receiveProgress;

        public ConnectedViewModel() {
            sendProgress = new Progress<SendFileProgressReport>();
            sendProgress.ProgressChanged += UploadReport;

            receiveProgress = new Progress<ReceiveFileProgressReport>();
            receiveProgress.ProgressChanged += DownloadReport;
        }

        public FileSharingClient FileSharingClient { get; set; }

        public bool Sending {
            get { return _sending; }
            set {
                if (_sending == value) {
                    return;
                }

                _sending = value;
                NotifyOfPropertyChange(() => CanReceive);
                NotifyOfPropertyChange(() => CanSend);
            }
        }

        public bool Receiving {
            get { return _receiving; }
            set {
                if (_receiving == value) {
                    return;
                }

                _receiving = value;
                NotifyOfPropertyChange(() => CanReceive);
                NotifyOfPropertyChange(() => CanSend);
            }
        }

        public long Download {
            get { return _download; }
            set {
                if (_download == value) {
                    return;
                }

                _download = value;
                NotifyOfPropertyChange(() => Download);
            }
        }

        public long Upload {
            get { return _upload; }
            set {
                if (_upload == value) {
                    return;
                }

                _upload = value;
                NotifyOfPropertyChange(() => Upload);
            }
        }

        public bool CanReceive => !(Sending || Receiving);

        public async void Receive() {
            await FileSharingClient.ReceiveFileAsync(@"D:\Downloads", receiveProgress, default);
        }

        public bool CanSend => !(Sending || Receiving);

        public async void Send() {
            string path =
                @"D:\Downloads\Torrents\Series\Game of Thrones - Игра престолов. Сезон 8. Amedia. 1080p\Game.of.Thrones.s08e04.WEB-DL.1080p.Amedia.mkv";
            await FileSharingClient.SendFileAsync(path, sendProgress, default);
        }

        private void DownloadReport(object sender, ReceiveFileProgressReport e) {
            Download = e.FileData.FileSize / e.BytesRecived;
            Debug.WriteLine(e.ReceiveFileState);
        }

        private void UploadReport(object sender, SendFileProgressReport e) {
            Upload = e.FileData.FileSize / e.BytesSent;
            Debug.WriteLine(e.SendFileState);
        }
    }
}
