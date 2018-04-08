using ESDataBase.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EducationSystem.Services
{
    public class TestService : ITestService
    {
        private Test _test;
        public bool IsStarted
        {
            get;
            private set;
        }
        public int TrueAnswers
        {
            get;
            private set;
        }
        private QuestionEnumerator questionEnumerator;

        public TestService(Test test)
        {
            _test = test;
            questionEnumerator = new QuestionEnumerator(_test.Questions.ToList());
        }

        public void StartTest()
        {
            if (IsStarted)
                StopTest();

            IsStarted = true;
            TrueAnswers = 0;
        }

        public Question GetNextQuestion()
        {
            if (!IsStarted)
                StartTest();
            if (questionEnumerator.MoveNext())
                return questionEnumerator.Current;
            else
                StopTest();
            return null;
        }

        public void CheckAnswer(int rightNumber)
        {
            if (questionEnumerator.Current.RightAnswerNumber == rightNumber && IsStarted)
                ++TrueAnswers;   
        }

        public void StopTest()
        {
            IsStarted = false;
            questionEnumerator.Reset();
        }
    }
}