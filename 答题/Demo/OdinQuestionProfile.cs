using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Demo
{
    [CreateAssetMenu(fileName = "New Questions Profile", menuName = "question Profile1")]
    public class OdinQuestionProfile : ScriptableObject
    {
        [ListDrawerSettings] public List<QuestionDataBase.JudgeQuestion> judgeQuestions = new List<QuestionDataBase.JudgeQuestion>();
        /// <summary>
        /// 单向选择题列表
        /// </summary>
        [ListDrawerSettings]   public List<QuestionDataBase.SingleChoiceQuestion> singleChoices = new List<QuestionDataBase.SingleChoiceQuestion>(0);

        /// <summary>
        /// 多项选择题列表
        /// </summary>
        [ListDrawerSettings]    public List<QuestionDataBase.MultipleChoiceQuestion> multipleChoices = new List<QuestionDataBase.MultipleChoiceQuestion>(0);

        /// <summary>
        /// 填空题列表
        /// </summary>
        [ListDrawerSettings]  public List<QuestionDataBase.CompletionQuestion> completions = new List<QuestionDataBase.CompletionQuestion>(0);

        /// <summary>
        /// 论述题列表
        /// </summary>
        [ListDrawerSettings]   public List<QuestionDataBase.EssayQuestion> essays = new List<QuestionDataBase.EssayQuestion>();
    }
}