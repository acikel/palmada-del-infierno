using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

[System.Serializable]
public class Chapter
{
    public List<TextAsset> stories = new List<TextAsset>();

    public int Count()
    {
        return stories.Count;
    }

    public TextAsset GetStory(int _storyIndex)
    {
        return stories[_storyIndex];
    }
}

public class StoryManager : MonoBehaviour
{

    [SerializeField] private List<Chapter> chapters = new List<Chapter>();

    private int chapterIndex = 0;
    private int storyIndex = 0;

    [SerializeField] private int lastLinearChapter = 2;
    [SerializeField] private int BJDoboChapter = 2;
    [SerializeField] private int BhomasTuchelinChapter = 3;

    // PLACEHOLDER SCORE
    private int loveScore = 0;

    public TextAsset NextStory()
    {
        if(storyIndex + 1 < chapters[chapterIndex].Count())
        {
            storyIndex++;
        }
        else
        {
            storyIndex = 0;
            if(chapterIndex < lastLinearChapter)
            {
                chapterIndex++;
            }
            else
            {
                // all linear chapters finished
                // get chapter according to score
                if(loveScore < 0)
                {
                    chapterIndex = BJDoboChapter;
                }
                else
                {
                    chapterIndex = BhomasTuchelinChapter;
                }
            }
        }
        return GetCurrentStory();
    }

    public TextAsset GetCurrentStory()
    {
        return chapters[chapterIndex].GetStory(storyIndex);
    }

    public void UpdateLoveMeter(object _value)
    {
        loveScore += (int)_value;
    }
}
