using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SchoolSubjectGrade { None, F, D, DPlus, CMinus, C, CPlus, BMinus, B, BPlus, AMinus, A, APlus };

public enum SchoolSubjectCreditType { None, Exam, Essay };

[CreateAssetMenu(fileName = "New subject", menuName = "School system/New subject")]
public class SchoolSubject_SO : ScriptableObject
{
    #region Variables

    public SchoolSubjectGrade subjectGrade = SchoolSubjectGrade.None;
    public SchoolSubjectCreditType subjectCredit = SchoolSubjectCreditType.None;
    public string subjectName = "New Subject";
    public string subjectLetterGrade = "";

    // Analyse using scriptable objects for characters
    public GameObject subjectTeacher = null;

    public int maxPoints = 0;
    public int currentPoints = 0;

    public bool setManually = false;
    public bool isActive = false;

    #endregion
}
