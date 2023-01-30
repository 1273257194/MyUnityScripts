using System.Collections.Generic;
using UnityEditor;
using UnityEngine; 

namespace Demo
{
    [CreateAssetMenu(fileName = "New Questions Profile", menuName = "question Profile")]
    public class QuestionsProfile : ScriptableObject
    {
        /// <summary>
        /// 判断题列表
        /// </summary>
        public List<QuestionDataBase.JudgeQuestion> judges = new List<QuestionDataBase.JudgeQuestion>(0);

        /// <summary>
        /// 单向选择题列表
        /// </summary>
        public List<QuestionDataBase.SingleChoiceQuestion> singleChoices = new List<QuestionDataBase.SingleChoiceQuestion>(0);

        /// <summary>
        /// 多项选择题列表
        /// </summary>
        public List<QuestionDataBase.MultipleChoiceQuestion> multipleChoices = new List<QuestionDataBase.MultipleChoiceQuestion>(0);

        /// <summary>
        /// 填空题列表
        /// </summary>
        public List<QuestionDataBase.CompletionQuestion> completions = new List<QuestionDataBase.CompletionQuestion>(0);

        /// <summary>
        /// 论述题列表
        /// </summary>
        public List<QuestionDataBase.EssayQuestion> essays = new List<QuestionDataBase.EssayQuestion>();

        public T Get<T>(QuestionDataBase.QuestionType type, int sequence) where T : QuestionDataBase.QuestionBase
        {
            switch (type)
            {
                case QuestionDataBase.QuestionType.Judge: return judges.Find(m => m.sequence == sequence) as T;
                case QuestionDataBase.QuestionType.SingleChoice: return singleChoices.Find(m => m.sequence == sequence) as T;
                case QuestionDataBase.QuestionType.MultipleChoice: return multipleChoices.Find(m => m.sequence == sequence) as T;
                case QuestionDataBase.QuestionType.Completion: return completions.Find(m => m.sequence == sequence) as T;
                case QuestionDataBase.QuestionType.Essay: return essays.Find(m => m.sequence == sequence) as T;
                default: return null;
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(QuestionsProfile))]
    public sealed class QuestionsProfileInspector : Editor
    {
        private QuestionsProfile profile;
        private QuestionDataBase.QuestionType currentType;
        private readonly Color btnNormalColor = new Color(0.5f, 0.5f, 0.5f, 1f);
        private readonly GUIContent deleteContent = new GUIContent("-", "delete");
        private Vector2 scroll = Vector2.zero;
        private Dictionary<QuestionDataBase.JudgeQuestion, bool> judgeFoldoutMap;
        private Dictionary<QuestionDataBase.SingleChoiceQuestion, bool> singleChoicesFoldoutMap;
        private Dictionary<QuestionDataBase.MultipleChoiceQuestion, bool> multipleChoicesFoldoutMap;
        private Dictionary<QuestionDataBase.CompletionQuestion, bool> completionFoldoutMap;
        private Dictionary<QuestionDataBase.EssayQuestion, bool> essayFoldoutMap;

        private void OnEnable()
        {
            profile = target as QuestionsProfile;
            judgeFoldoutMap = new Dictionary<QuestionDataBase.JudgeQuestion, bool>();
            singleChoicesFoldoutMap = new Dictionary<QuestionDataBase.SingleChoiceQuestion, bool>();
            multipleChoicesFoldoutMap = new Dictionary<QuestionDataBase.MultipleChoiceQuestion, bool>();
            completionFoldoutMap = new Dictionary<QuestionDataBase.CompletionQuestion, bool>();
            essayFoldoutMap = new Dictionary<QuestionDataBase.EssayQuestion, bool>();
        }

        public override void OnInspectorGUI()
        {
            OnTypeGUI();
            OnMenuGUI();
            OnDetailGUI();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(profile);
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void OnTypeGUI()
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUI.color = currentType == QuestionDataBase.QuestionType.Judge ? Color.white : btnNormalColor;
                if (GUILayout.Button("判断题", EditorStyles.miniButtonLeft)) currentType = QuestionDataBase.QuestionType.Judge;
                GUI.color = currentType == QuestionDataBase.QuestionType.SingleChoice ? Color.white : btnNormalColor;
                if (GUILayout.Button("单选题", EditorStyles.miniButtonMid)) currentType = QuestionDataBase.QuestionType.SingleChoice;
                GUI.color = currentType == QuestionDataBase.QuestionType.MultipleChoice ? Color.white : btnNormalColor;
                if (GUILayout.Button("多选题", EditorStyles.miniButtonMid)) currentType = QuestionDataBase.QuestionType.MultipleChoice;
                GUI.color = currentType == QuestionDataBase.QuestionType.Completion ? Color.white : btnNormalColor;
                if (GUILayout.Button("填空题", EditorStyles.miniButtonMid)) currentType = QuestionDataBase.QuestionType.Completion;
                GUI.color = currentType == QuestionDataBase.QuestionType.Essay ? Color.white : btnNormalColor;
                if (GUILayout.Button("论述题", EditorStyles.miniButtonRight)) currentType = QuestionDataBase.QuestionType.Essay;
                GUI.color = Color.white;
            }
            EditorGUILayout.EndHorizontal();
        }

        private void OnMenuGUI()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("展开", EditorStyles.miniButtonLeft))
            {
                switch (currentType)
                {
                    case QuestionDataBase.QuestionType.Judge:
                        profile.judges.ForEach(m => judgeFoldoutMap[m] = true);
                        break;
                    case QuestionDataBase.QuestionType.SingleChoice:
                        profile.singleChoices.ForEach(m => singleChoicesFoldoutMap[m] = true);
                        break;
                    case QuestionDataBase.QuestionType.MultipleChoice:
                        profile.multipleChoices.ForEach(m => multipleChoicesFoldoutMap[m] = true);
                        break;
                    case QuestionDataBase.QuestionType.Completion:
                        profile.completions.ForEach(m => completionFoldoutMap[m] = true);
                        break;
                    case QuestionDataBase.QuestionType.Essay:
                        profile.essays.ForEach(m => essayFoldoutMap[m] = true);
                        break;
                }
            }

            if (GUILayout.Button("收缩", EditorStyles.miniButtonMid))
            {
                switch (currentType)
                {
                    case QuestionDataBase.QuestionType.Judge:
                        profile.judges.ForEach(m => judgeFoldoutMap[m] = false);
                        break;
                    case QuestionDataBase.QuestionType.SingleChoice:
                        profile.singleChoices.ForEach(m => singleChoicesFoldoutMap[m] = false);
                        break;
                    case QuestionDataBase.QuestionType.MultipleChoice:
                        profile.multipleChoices.ForEach(m => multipleChoicesFoldoutMap[m] = false);
                        break;
                    case QuestionDataBase.QuestionType.Completion:
                        profile.completions.ForEach(m => completionFoldoutMap[m] = false);
                        break;
                    case QuestionDataBase.QuestionType.Essay:
                        profile.essays.ForEach(m => essayFoldoutMap[m] = false);
                        break;
                }
            }

            if (GUILayout.Button("添加", EditorStyles.miniButtonMid))
            {
                Undo.RecordObject(profile, "Add New");
                switch (currentType)
                {
                    case QuestionDataBase.QuestionType.Judge:
                        profile.judges.Add(new QuestionDataBase.JudgeQuestion());
                        break;
                    case QuestionDataBase.QuestionType.SingleChoice:
                        profile.singleChoices.Add(new QuestionDataBase.SingleChoiceQuestion());
                        break;
                    case QuestionDataBase.QuestionType.MultipleChoice:
                        profile.multipleChoices.Add(new QuestionDataBase.MultipleChoiceQuestion());
                        break;
                    case QuestionDataBase.QuestionType.Completion:
                        profile.completions.Add(new QuestionDataBase.CompletionQuestion());
                        break;
                    case QuestionDataBase.QuestionType.Essay:
                        profile.essays.Add(new QuestionDataBase.EssayQuestion());
                        break;
                }
            }

            if (GUILayout.Button("清空", EditorStyles.miniButtonRight))
            {
                Undo.RecordObject(profile, "Clear");
                if (EditorUtility.DisplayDialog("Prompt", "Are you sure clear the questions?", "Yes", "No"))
                {
                    switch (currentType)
                    {
                        case QuestionDataBase.QuestionType.Judge:
                            profile.judges.Clear();
                            judgeFoldoutMap.Clear();
                            break;
                        case QuestionDataBase.QuestionType.SingleChoice:
                            profile.singleChoices.Clear();
                            singleChoicesFoldoutMap.Clear();
                            break;
                        case QuestionDataBase.QuestionType.MultipleChoice:
                            profile.multipleChoices.Clear();
                            multipleChoicesFoldoutMap.Clear();
                            break;
                        case QuestionDataBase.QuestionType.Completion:
                            profile.completions.Clear();
                            completionFoldoutMap.Clear();
                            break;
                        case QuestionDataBase.QuestionType.Essay:
                            profile.essays.Clear();
                            essayFoldoutMap.Clear();
                            break;
                    }
                }
            }

            GUILayout.EndHorizontal();
        }

        private void OnDetailGUI()
        {
            scroll = GUILayout.BeginScrollView(scroll);
            switch (currentType)
            {
                #region 判断题

                case QuestionDataBase.QuestionType.Judge:
                    for (int i = 0; i < profile.judges.Count; i++)
                    {
                        var current = profile.judges[i];
                        if (!judgeFoldoutMap.ContainsKey(current)) judgeFoldoutMap.Add(current, true);

                        GUILayout.BeginHorizontal("IN Title");
                        judgeFoldoutMap[current] = EditorGUILayout.Foldout(judgeFoldoutMap[current], $"第 {current.sequence} 题", true);
                        if (GUILayout.Button("×", GUILayout.Width(20f)))
                        {
                            profile.judges.Remove(current);
                            judgeFoldoutMap.Remove(current);
                            break;
                        }

                        GUILayout.EndHorizontal();
                        if (judgeFoldoutMap[current])
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("题号：", GUILayout.Width(40));
                            var newValue = EditorGUILayout.IntField(current.sequence, GUILayout.Width(30));
                            if (current.sequence != newValue)
                            {
                                Undo.RecordObject(profile, "Judge sequence");
                                current.sequence = newValue;
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("问题：", GUILayout.Width(40));
                            var newQ = EditorGUILayout.TextArea(current.question, new GUIStyle(GUI.skin.textArea) {stretchWidth = false}, GUILayout.ExpandWidth(true));
                            if (newQ != current.question)
                            {
                                Undo.RecordObject(profile, "Judge question");
                                current.question = newQ;
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("正确：", GUILayout.Width(40));
                            var newP = EditorGUILayout.TextField(current.positive);
                            if (newP != current.positive)
                            {
                                Undo.RecordObject(profile, "positive");
                                current.positive = newP;
                            }

                            var newAnswer = EditorGUILayout.Toggle(current.answer, GUILayout.Width(15));
                            if (newAnswer != (current.answer))
                            {
                                Undo.RecordObject(profile, "Judge answer");
                                current.answer = true;
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("错误：", GUILayout.Width(40));
                            var newN = EditorGUILayout.TextField(current.negative);
                            if (newN != current.positive)
                            {
                                Undo.RecordObject(profile, "negative");
                                current.negative = newN;
                            }

                            var newAns = EditorGUILayout.Toggle(current.answer == false, GUILayout.Width(15));
                            if (newAns != (current.answer == false))
                            {
                                Undo.RecordObject(profile, "Judge answer");
                                current.answer = false;
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("题解：", GUILayout.Width(40));
                            var newA = EditorGUILayout.TextArea(current.analysis, new GUIStyle(GUI.skin.textArea) {stretchWidth = false}, GUILayout.ExpandWidth(true));
                            if (newA != current.analysis)
                            {
                                Undo.RecordObject(profile, "Judge analysis");
                                current.analysis = newA;
                            }

                            GUILayout.EndHorizontal();
                        }
                    }

                    break;

                #endregion

                #region 单选题

                case QuestionDataBase.QuestionType.SingleChoice:
                    for (int i = 0; i < profile.singleChoices.Count; i++)
                    {
                        var current = profile.singleChoices[i];
                        if (!singleChoicesFoldoutMap.ContainsKey(current)) singleChoicesFoldoutMap.Add(current, true);

                        GUILayout.BeginHorizontal("IN Title");
                        singleChoicesFoldoutMap[current] = EditorGUILayout.Foldout(singleChoicesFoldoutMap[current], $"第 {current.sequence} 题", true);
                        if (GUILayout.Button("×", GUILayout.Width(20f)))
                        {
                            profile.singleChoices.Remove(current);
                            singleChoicesFoldoutMap.Remove(current);
                            break;
                        }

                        GUILayout.EndHorizontal();

                        if (singleChoicesFoldoutMap[current])
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("题号：", GUILayout.Width(40));
                            var newS = EditorGUILayout.IntField(current.sequence, GUILayout.Width(30));
                            if (current.sequence != newS)
                            {
                                Undo.RecordObject(profile, "SingleChoices sequence");
                                current.sequence = newS;
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("问题：", GUILayout.Width(40));
                            var newQ = EditorGUILayout.TextArea(current.question, new GUIStyle(GUI.skin.textArea) {stretchWidth = false}, GUILayout.ExpandWidth(true));
                            if (newQ != current.question)
                            {
                                Undo.RecordObject(profile, "SingleChoices question");
                                current.question = newQ;
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("选项：", GUILayout.Width(40));
                            GUILayout.BeginHorizontal();
                            if (GUILayout.Button("＋", GUILayout.Width(25)))
                            {
                                Undo.RecordObject(profile, "SingleChoices Add");
                                current.choices.Add(new QuestionDataBase.QuestionChoice("选项描述", null));
                            }

                            GUILayout.FlexibleSpace();
                            current.choiceType = (QuestionDataBase.ChoiceType) EditorGUILayout.EnumPopup(current.choiceType, GUILayout.Width(80f));
                            GUILayout.Label("(类型)");
                            GUILayout.EndHorizontal();
                            GUILayout.EndHorizontal();

                            GUILayout.BeginVertical();
                            for (int k = 0; k < current.choices.Count; k++)
                            {
                                GUILayout.BeginHorizontal();
                                GUILayout.Space(50);
                                GUILayout.Label($"{QuestionDataBase.Alphabet.values[k]}.", GUILayout.Width(20));
                                switch (current.choiceType)
                                {
                                    case QuestionDataBase.ChoiceType.Text:
                                        current.choices[k].text = GUILayout.TextField(current.choices[k].text);
                                        break;
                                    case QuestionDataBase.ChoiceType.Pic:
                                        current.choices[k].pic = EditorGUILayout.ObjectField(current.choices[k].pic, typeof(Sprite), false) as Sprite;
                                        break;
                                    case QuestionDataBase.ChoiceType.TextAndPic:
                                        current.choices[k].text = GUILayout.TextField(current.choices[k].text);
                                        current.choices[k].pic = EditorGUILayout.ObjectField(current.choices[k].pic, typeof(Sprite), false, GUILayout.Width(110f)) as Sprite;
                                        break;
                                }

                                var newValue = EditorGUILayout.Toggle(current.answer == k, GUILayout.Width(15));
                                if (newValue)
                                {
                                    Undo.RecordObject(profile, "SingleChoices answer");
                                    current.answer = k;
                                }

                                if (GUILayout.Button(deleteContent, "MiniButton", GUILayout.Width(18)))
                                {
                                    Undo.RecordObject(profile, "Delete SingleChoice");
                                    current.choices.RemoveAt(k);
                                    break;
                                }

                                GUILayout.EndHorizontal();
                            }

                            GUILayout.EndVertical();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("题解：", GUILayout.Width(40));
                            var newA = EditorGUILayout.TextArea(current.analysis, new GUIStyle(GUI.skin.textArea) {stretchWidth = false}, GUILayout.ExpandWidth(true));
                            if (newA != current.analysis)
                            {
                                Undo.RecordObject(profile, "SingleChoices analysis");
                                current.analysis = newA;
                            }

                            GUILayout.EndHorizontal();
                        }
                    }

                    break;

                #endregion

                #region 多选题

                case QuestionDataBase.QuestionType.MultipleChoice:
                    for (int i = 0; i < profile.multipleChoices.Count; i++)
                    {
                        var current = profile.multipleChoices[i];
                        if (!multipleChoicesFoldoutMap.ContainsKey(current)) multipleChoicesFoldoutMap.Add(current, true);

                        GUILayout.BeginHorizontal("IN Title");
                        multipleChoicesFoldoutMap[current] = EditorGUILayout.Foldout(multipleChoicesFoldoutMap[current], $"第 {current.sequence} 题", true);
                        if (GUILayout.Button("×", GUILayout.Width(20f)))
                        {
                            profile.multipleChoices.Remove(current);
                            multipleChoicesFoldoutMap.Remove(current);
                            break;
                        }

                        GUILayout.EndHorizontal();

                        if (multipleChoicesFoldoutMap[current])
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("题号：", GUILayout.Width(40));
                            var newS = EditorGUILayout.IntField(current.sequence, GUILayout.Width(30));
                            if (newS != current.sequence)
                            {
                                Undo.RecordObject(profile, "MultipleChoices sequence");
                                current.sequence = newS;
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("问题：", GUILayout.Width(40));
                            var newQ = EditorGUILayout.TextArea(current.question, new GUIStyle(GUI.skin.textArea) {stretchWidth = false}, GUILayout.ExpandWidth(true));
                            if (newQ != current.question)
                            {
                                Undo.RecordObject(profile, "MultipleChoices question");
                                current.question = newQ;
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("选项：", GUILayout.Width(40));
                            GUILayout.BeginHorizontal();
                            if (GUILayout.Button("＋", GUILayout.Width(25)))
                            {
                                Undo.RecordObject(profile, "SingleChoices Add");
                                current.choices.Add(new QuestionDataBase.QuestionChoice("选项描述", null));
                            }

                            GUILayout.FlexibleSpace();
                            current.choiceType = (QuestionDataBase.ChoiceType) EditorGUILayout.EnumPopup(current.choiceType, GUILayout.Width(80f));
                            GUILayout.Label("(类型)");
                            GUILayout.EndHorizontal();
                            GUILayout.EndHorizontal();

                            GUILayout.BeginVertical();
                            for (int k = 0; k < current.choices.Count; k++)
                            {
                                GUILayout.BeginHorizontal();
                                GUILayout.Space(50);
                                GUILayout.Label($"{QuestionDataBase.Alphabet.values[k]}.", GUILayout.Width(20));
                                switch (current.choiceType)
                                {
                                    case QuestionDataBase.ChoiceType.Text:
                                        current.choices[k].text = GUILayout.TextField(current.choices[k].text);
                                        break;
                                    case QuestionDataBase.ChoiceType.Pic:
                                        current.choices[k].pic = EditorGUILayout.ObjectField(current.choices[k].pic, typeof(Sprite), false) as Sprite;
                                        break;
                                    case QuestionDataBase.ChoiceType.TextAndPic:
                                        current.choices[k].text = GUILayout.TextField(current.choices[k].text);
                                        current.choices[k].pic = EditorGUILayout.ObjectField(current.choices[k].pic, typeof(Sprite), false, GUILayout.Width(110f)) as Sprite;
                                        break;
                                }

                                var newValue = EditorGUILayout.Toggle(current.answers.Contains(k), GUILayout.Width(15));
                                if (newValue != current.answers.Contains(k))
                                {
                                    Undo.RecordObject(profile, "MultipleChoices answers");
                                    if (newValue)
                                        current.answers.Add(k);
                                    else
                                        current.answers.Remove(k);
                                }

                                if (GUILayout.Button(deleteContent, "MiniButton", GUILayout.Width(18)))
                                {
                                    Undo.RecordObject(profile, "Delete MultipleChoice");
                                    current.choices.RemoveAt(k);
                                    break;
                                }

                                GUILayout.EndHorizontal();
                            }

                            GUILayout.EndVertical();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("题解：", GUILayout.Width(40));
                            var newA = EditorGUILayout.TextArea(current.analysis, new GUIStyle(GUI.skin.textArea) {stretchWidth = false}, GUILayout.ExpandWidth(true));
                            if (newA != current.analysis)
                            {
                                Undo.RecordObject(profile, "MultipleChoices analysis");
                                current.analysis = newA;
                            }

                            GUILayout.EndHorizontal();
                        }
                    }

                    break;

                #endregion

                #region 填空题

                case QuestionDataBase.QuestionType.Completion:
                    for (int i = 0; i < profile.completions.Count; i++)
                    {
                        var current = profile.completions[i];
                        if (!completionFoldoutMap.ContainsKey(current)) completionFoldoutMap.Add(current, true);

                        GUILayout.BeginHorizontal("IN Title");
                        completionFoldoutMap[current] = EditorGUILayout.Foldout(completionFoldoutMap[current], $"第 {current.sequence} 题", true);
                        if (GUILayout.Button("×", GUILayout.Width(20f)))
                        {
                            profile.completions.Remove(current);
                            completionFoldoutMap.Remove(current);
                            break;
                        }

                        GUILayout.EndHorizontal();

                        if (completionFoldoutMap[current])
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("题号：", GUILayout.Width(40));
                            var newS = EditorGUILayout.IntField(current.sequence, GUILayout.Width(30));
                            if (newS != current.sequence)
                            {
                                Undo.RecordObject(profile, "Completion sequence");
                                current.sequence = newS;
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("问题：", GUILayout.Width(40));
                            var newQ = EditorGUILayout.TextArea(current.question, new GUIStyle(GUI.skin.textArea) {stretchWidth = false}, GUILayout.ExpandWidth(true));
                            if (newQ != current.question)
                            {
                                Undo.RecordObject(profile, "Completion question");
                                current.question = newQ;
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("答案：", GUILayout.Width(40));
                            if (GUILayout.Button("＋", GUILayout.Width(25)))
                            {
                                Undo.RecordObject(profile, "CompletionAnswers Add");
                                current.answers.Add(null);
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginVertical();
                            for (int n = 0; n < current.answers.Count; n++)
                            {
                                GUILayout.BeginHorizontal();
                                GUILayout.Space(50);
                                GUILayout.Label($"({n + 1}).", GUILayout.Width(30));
                                var newC = EditorGUILayout.TextField(current.answers[n]);
                                if (current.answers[n] != newC)
                                {
                                    Undo.RecordObject(profile, "CompletionAnswer");
                                    current.answers[n] = newC;
                                }

                                if (GUILayout.Button(deleteContent, "MiniButton", GUILayout.Width(18)))
                                {
                                    Undo.RecordObject(profile, "CompletionAnswers Remove");
                                    current.answers.RemoveAt(n);
                                    break;
                                }

                                GUILayout.EndHorizontal();
                            }

                            GUILayout.EndVertical();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("题解：", GUILayout.Width(40));
                            var newA = EditorGUILayout.TextArea(current.analysis, new GUIStyle(GUI.skin.textArea) {stretchWidth = false}, GUILayout.ExpandWidth(true));
                            if (newA != current.analysis)
                            {
                                Undo.RecordObject(profile, "Completion analysis");
                                current.analysis = newA;
                            }

                            GUILayout.EndHorizontal();
                        }
                    }

                    break;

                #endregion

                #region 论述题

                case QuestionDataBase.QuestionType.Essay:
                    for (int i = 0; i < profile.essays.Count; i++)
                    {
                        var current = profile.essays[i];
                        if (!essayFoldoutMap.ContainsKey(current)) essayFoldoutMap.Add(current, true);

                        GUILayout.BeginHorizontal("IN Title");
                        essayFoldoutMap[current] = EditorGUILayout.Foldout(essayFoldoutMap[current], $"第 {current.sequence} 题", true);
                        if (GUILayout.Button("×", GUILayout.Width(20f)))
                        {
                            profile.essays.Remove(current);
                            essayFoldoutMap.Remove(current);
                            break;
                        }

                        GUILayout.EndHorizontal();

                        if (essayFoldoutMap[current])
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("题号：", GUILayout.Width(40));
                            var newS = EditorGUILayout.IntField(current.sequence, GUILayout.Width(30));
                            if (newS != current.sequence)
                            {
                                Undo.RecordObject(profile, "Essay sequence");
                                current.sequence = newS;
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("问题：", GUILayout.Width(40));
                            var newQ = EditorGUILayout.TextArea(current.question, new GUIStyle(GUI.skin.textArea) {stretchWidth = false}, GUILayout.ExpandWidth(true));
                            if (newQ != current.question)
                            {
                                Undo.RecordObject(profile, "Essay question");
                                current.question = newQ;
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("答案：", GUILayout.Width(40));
                            var newA = EditorGUILayout.TextArea(current.answer, new GUIStyle(GUI.skin.textArea) {stretchWidth = false}, GUILayout.ExpandWidth(true));
                            if (newA != current.answer)
                            {
                                Undo.RecordObject(profile, "Essay answer");
                                current.answer = newA;
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("题解：", GUILayout.Width(40));
                            var newV = EditorGUILayout.TextArea(current.analysis, new GUIStyle(GUI.skin.textArea) {stretchWidth = false}, GUILayout.ExpandWidth(true));
                            if (newV != current.analysis)
                            {
                                Undo.RecordObject(profile, "Essay analysis");
                                current.analysis = newV;
                            }

                            GUILayout.EndHorizontal();
                        }
                    }

                    break;

                #endregion

                default:
                    GUILayout.Label("Unknown question Type.");
                    break;
            }

            GUILayout.EndScrollView();
        }
    }
#endif
}