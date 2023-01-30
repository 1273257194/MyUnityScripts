using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Demo
{
    public class QuestionDataBase
    {
        public enum QuestionType
        {
            /// <summary>
            /// 判断题
            /// </summary>
            Judge,

            /// <summary>
            /// 单选题
            /// </summary>
            SingleChoice,

            /// <summary>
            /// 多选题
            /// </summary>
            MultipleChoice,

            /// <summary>
            /// 填空题
            /// </summary>
            Completion,

            /// <summary>
            /// 论述题
            /// </summary>
            Essay,
        }

        public class QuestionBase
        {
            /// <summary>
            /// 题号
            /// </summary>
            public int sequence;

            /// <summary>
            /// 问题
            /// </summary>
            public string question;

            /// <summary>
            /// 题型
            /// </summary>
            public QuestionType type;

            /// <summary>
            /// 题解
            /// </summary>
            public string analysis;
        }

        /// <summary>
        /// 判断题
        /// </summary>
        [Serializable]
        public class JudgeQuestion : QuestionBase
        {
            /// <summary>
            /// 积极选项
            /// </summary>
            public string positive = "正确";

            /// <summary>
            /// 消极选项
            /// </summary>
            public string negative = "错误";

            /// <summary>
            /// 答案
            /// </summary>
            public bool answer;
        }

        /// <summary>
        /// 填空题
        /// </summary>
        [Serializable]
        public class CompletionQuestion : QuestionBase
        {
            /// <summary>
            /// 答案
            /// </summary>
            public List<string> answers = new List<string>(0);
        }

        /// <summary>
        /// 论述题
        /// </summary>
        [Serializable]
        public class EssayQuestion : QuestionBase
        {
            /// <summary>
            /// 答案
            /// </summary>
            public string answer;
        }

        /// <summary>
        /// 选项类型
        /// </summary>
        public enum ChoiceType
        {
            /// <summary>
            /// 文本
            /// </summary>
            Text,

            /// <summary>
            /// 图片
            /// </summary>
            Pic,

            /// <summary>
            /// 文本+图片
            /// </summary>
            TextAndPic
        }

        [Serializable]
        public class QuestionChoice
        {
            public string sequence;
            public string text;
            public Sprite pic;

            public QuestionChoice(string sequence, Sprite pic)
            {
                this.pic = pic;
                this.sequence = sequence;
            }
        }

        /// <summary>
        /// 单项选择题
        /// </summary>
        [Serializable]
        public class SingleChoiceQuestion : QuestionBase
        {
            /// <summary>
            /// 选项类型
            /// </summary>
            public ChoiceType choiceType;

            /// <summary>
            /// 选项
            /// </summary>
            public List<QuestionChoice> choices = new List<QuestionChoice>(0);

            /// <summary>
            /// 答案
            /// </summary>
            public int answer;
        }

        /// <summary>
        /// 多项选择题
        /// </summary>
        [Serializable]
        public class MultipleChoiceQuestion : QuestionBase
        {
            /// <summary>
            /// 选项类型
            /// </summary>
            public ChoiceType choiceType;

            /// <summary>
            /// 选项
            /// </summary>
            public List<QuestionChoice> choices = new List<QuestionChoice>(0);

            /// <summary>
            /// 答案
            /// </summary>
            public List<int> answers = new List<int>(0);
        }

        /// <summary>
        /// 英文字母表
        /// </summary>
        public static class Alphabet
        {
            public static char[] values = new char[26]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'G', 'K', 'L', 'M',
                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            };
        }
    }
}