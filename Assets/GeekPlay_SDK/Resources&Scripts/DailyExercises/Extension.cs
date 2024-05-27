using System.Collections.Generic;

namespace Exercise
{
    public static class Extension
    {
        public static DailyExercise GetValueByKey(this List<ExerciseDictionary> myDictionaries, Days day)
        {
            foreach (ExerciseDictionary item in myDictionaries)
            {
                if (item.day == day)
                {
                    return item.dailyExercise;
                }
            }
            return null;
        }
    }
}