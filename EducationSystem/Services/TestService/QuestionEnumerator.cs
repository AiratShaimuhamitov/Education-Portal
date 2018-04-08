using ESDataBase.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Services
{
    public class QuestionEnumerator : IEnumerator<Question>
    {
        private List<Question> _questions;
        private int curIndex;
        private Question curQuestion;


        public QuestionEnumerator(List<Question> questions)
        {
            _questions = questions;
            curIndex = -1;
            curQuestion = default(Question);
        }

        void IDisposable.Dispose() { }

        public bool MoveNext()
        {
            if (++curIndex >= _questions.Count)
                return false;
            else
                curQuestion = _questions[curIndex];
            return true;
        }

        public void Reset()
        {
            curIndex = -1;
        }

        public Question Current
        {
            get { return curQuestion; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}