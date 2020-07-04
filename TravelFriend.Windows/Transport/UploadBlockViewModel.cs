using System;
using System.Collections.Generic;
using System.Text;
using TravelFriend.Windows.Http.BreakPoint;

namespace TravelFriend.Windows.Transport
{
    public class UploadBlockViewModel : BaseViewModel
    {
        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                Change(nameof(FileName));
            }
        }

        private string _fileSize;
        public string FileSize
        {
            get
            {
                return _fileSize;
            }
            set
            {
                _fileSize = value + "MB";
                Change(nameof(FileSize));
            }
        }

        private FileType _fileType;
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType
        {
            get
            {
                return _fileType;
            }
            set
            {
                _fileType = value;
                Change(nameof(FileType));
            }
        }

        private string _target;
        /// <summary>
        /// 目标（团队/用户的相册）
        /// </summary>
        public string Target
        {
            get
            {
                return _target;
            }
            set
            {
                _target = value;
                Change(nameof(Target));
            }
        }

        /// <summary>
        /// 剩余时间(s秒)
        /// </summary>
        public int Time
        {
            set
            {
                if (value <= 0)
                {
                    Timestamp = $"00:00:01";
                }
                else
                {
                    int hour = value / 3600;
                    int min = (value - hour * 3600) / 60;
                    int sen = value - hour * 3600 - min * 60;
                    Timestamp = $"{hour}:{min}:{sen}";
                }
            }
        }

        private string _timestamp = "--.--";
        /// <summary>
        /// 剩余时间戳
        /// </summary>
        public string Timestamp
        {
            get
            {
                return _timestamp;
            }
            set
            {
                _timestamp = value;
                Change(nameof(Timestamp));
            }
        }

        private string _speed = "--.--";
        /// <summary>
        /// 速度
        /// </summary>
        public string Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
                Change(nameof(Speed));
            }
        }

        private int _progress;
        /// <summary>
        /// 进度条进度
        /// </summary>
        public int Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                _progress = value;
                Change(nameof(Progress));
            }
        }
    }
}
