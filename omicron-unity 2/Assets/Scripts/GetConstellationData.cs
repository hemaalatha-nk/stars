



using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetConstellationData : MonoBehaviour
{
    public enum ConstellationDataset
    {
        Modern,
        Arabic,
        Norse,
        Egyptian,
        Chinese,
        Maya
    }

    public TextAsset modernConstellationFile;
    //public TextAsset modernConstellationNamesFile;


    public TextAsset egyptianConstellationFile;
    //public TextAsset egyptianConstellationNamesFile;

    public TextAsset chineseConstellationFile;
    //public TextAsset chineseConstellationNamesFile;


    public TextAsset arabicConstellationFile;
    //public TextAsset arabicConstellationNamesFile;

    public TextAsset indianConstellationFile;
    //public TextAsset indianConstellationNamesFile;



    private Dictionary<string, string[]> datasetMap = new Dictionary<string, string[]>();

    public string SelectedConstellationDataset { get; private set; }

    public DrawContellation drawContellation;
    public Text text;
    public TMP_Text text2;

    void Start()
    {
        // Populate the dataset map
        datasetMap.Add("modern", (GetLines(modernConstellationFile)));
        datasetMap.Add("arabic", (GetLines(arabicConstellationFile)));
        datasetMap.Add("indian", (GetLines(indianConstellationFile)));
        datasetMap.Add("egyptian", (GetLines(egyptianConstellationFile)));
        datasetMap.Add("chinese", (GetLines(chineseConstellationFile)));


        buttonConst("modern");

    }

    string[] GetLines(TextAsset textAsset)
    {
        if (textAsset != null)
        {
            string[] lines = textAsset.text.Split('\n');
            Debug.Log(lines[0]);
            return lines;
        }
        else
        {
            Debug.LogError("TextAsset is null.");
            return new string[0];
        }
    }




    public void buttonConst(string constellationName)
    {


        SelectedConstellationDataset = constellationName;
        drawContellation.SendMessage("UpdateConstellations");


    }




    public string[] GetConstellationFile()
    {


        if (SelectedConstellationDataset == "egyptian")
        {

            text.text = "Additional Info: \nThe constellations of ancient Egypt played a significant role in its culture, intertwining with mythology, religion, and daily life. Unlike the Greek constellations that are more widely recognized today, Egyptian constellations were often associated with gods and goddesses and had practical applications in agriculture and astronomy.\n\nOne of the most famous astronomical elements in ancient Egyptian culture is the star Sirius, known as \"Sopdet\" in Egyptian. Its heliacal rising (the first night it becomes visible just before sunrise after moving from behind the Sun) marked the beginning of the annual inundation of the Nile, which was crucial for agriculture. This event also signified the start of the Egyptian New Year.\n\nThe ancient Egyptians also recognized constellations that they associated with their deities and myths. For instance, the constellation we know as Orion was associated with Osiris, the god of the afterlife, resurrection, and fertility. The belt of Orion was seen as representing Osiris' phallus, which plays a significant role in the myth of his resurrection and the establishment of the cycles of life and death. The Milky Way was considered to be the celestial representation of the Nile River, further integrating the cosmos into their understanding of the world around them.";

        }
        else if(SelectedConstellationDataset == "modern")
        {
            text.text = "Additional Info: \nModern constellations are the 88 officially recognized groups of stars in the sky. These constellations cover the entire celestial sphere, as defined by the International Astronomical Union (IAU) in 1922. Unlike ancient constellations, which were derived from mythological and cultural origins and varied significantly across different civilizations, modern constellations have precisely defined boundaries. Each point in the sky lies within one of these 88 areas, making modern constellations an essential tool for astronomers to categorize and locate astronomical objects.\n\nThe modern constellations maintain many of the names and symbols from ancient Greek, Roman, and Middle Eastern traditions, but they also include constellations in the far southern sky that were not visible to the ancient astronomers of the northern hemisphere. These southern constellations were mostly introduced in the 16th to the 18th centuries as explorers charted the southern skies.\n\nThe IAU's demarcation and standardization of constellations with exact boundaries mean that any given point in the sky is associated with one, and only one, constellation. This system facilitates a more organized and universal method for naming stars and other celestial objects, aiding astronomers in mapping and discussing the heavens more effectively.";
        }
        else if (SelectedConstellationDataset == "inidan")
        {
            text.text = "Additional Info: \nIndian constellations, known as Nakshatras in Vedic astrology, form a vital part of Indian astronomical and astrological traditions. The concept of Nakshatras is deeply ingrained in Indian culture, dating back to the Vedic period (around 1500 – 500 BCE). Unlike the 88 constellations recognized by modern astronomy, which are based on Greek and Roman mythology, the Indian system identifies 27 (or 28 in some traditions) Nakshatras that span the ecliptic, each occupying a 13° 20' segment of the sky.\n\nThese Nakshatras serve as lunar mansions or sectors along the moon's path across the sky, making the moon’s movement a critical aspect of timekeeping and calendar formation in ancient India. Each Nakshatra is associated with a specific star or star cluster, and has its own mythological stories, attributes, and significance. They are believed to exert various influences, both auspicious and inauspicious, affecting daily life and events based on the moon's position within them.\n\nThe Nakshatras are also fundamental in Hindu astrology (Jyotisha), where they play a crucial role in determining the auspicious timing for various ceremonies, rituals, and actions (Muhurta). They influence personal characteristics, life events, and one's fate, as determined by the position of planets within these lunar mansions at the time of one's birth.\n\nApart from Nakshatras, Indian astronomy also acknowledges the zodiac constellations similar to those in Western astronomy, but the emphasis on lunar rather than solar movement marks a significant difference. The ancient Indian text on astronomy, the Vedanga Jyotisha, outlines the earliest known Indian astronomical observations, highlighting the importance of the Nakshatras and their role in the timing of Vedic rituals.\n\nIn summary, Indian constellations, or Nakshatras, are a unique blend of astronomy and astrology, deeply intertwined with cultural and religious practices. Their influence extends beyond mere observation of the night sky, permeating various aspects of life and spirituality in Indian society.";
        }
        else if (SelectedConstellationDataset == "arabic")
        {
            text.text = "Additional Info: \nArabic constellations are deeply intertwined with the rich history of astronomy during the Islamic Golden Age, a period spanning from the 8th to the 14th century. During this era, Arab astronomers made significant contributions to the field by translating, refining, and expanding upon the astronomical knowledge inherited from ancient Greek, Persian, and Indian cultures. This led to the development of a distinctive astronomical tradition that included the use of many star names and concepts that have been absorbed into modern astronomy.\n\nArabic scholars preserved and enhanced the constellation knowledge from earlier civilizations, such as those of Ptolemy’s Almagest, and added their own observations and innovations. They were particularly influential in the naming of stars. Many stars in today's night sky, such as Aldebaran, Altair, and Betelgeuse, retain the names given to them by Arabic astronomers. These names often reflect descriptions of the stars’ positions within their constellations or their brightness.\n\nIn addition to star names, Arabic astronomers were pioneers in the development of astronomical instruments, star maps, and navigational guides. They established observatories to refine the measurement of celestial bodies' positions and laid the groundwork for the modern field of observational astronomy.\n\nOne of the most famous works in this tradition is the Book of Fixed Stars, written by Abd al-Rahman al-Sufi in the 10th century. Al-Sufi's work not only described the classical Greek constellations and their stars but also introduced several new constellations that were not visible from the Mediterranean region. His book is also notable for its detailed observations of the Andromeda Galaxy and the Large Magellanic Cloud, which were among the first recorded observations of galaxies beyond the Milky Way.";
        }
        else if (SelectedConstellationDataset == "chinese")
        {
            text.text = "Additional Info: \nChinese constellations, known as \"Xingguan\" in Mandarin, represent one of the oldest and most intricate systems of astronomy in the world. Unlike the Western zodiac which is divided into 12 constellations, the Chinese sky was traditionally divided into 28 \"xiu\" (宿), which can be translated as \"mansions\" or \"lodges\". These are further grouped into four regions, each associated with one of the four cardinal directions and symbolized by a mythological creature: the Azure Dragon of the East, the Vermilion Bird of the South, the White Tiger of the West, and the Black Tortoise of the North, with the latter often depicted intertwined with a snake, representing the constellation grouping around the North Pole outside of the zodiac band.\n\nEach xiu corresponds to a specific segment of the lunar orbit around the Earth, thus serving a similar function to the lunar mansions of Indian and Arabic astronomy. This system played a crucial role in the Chinese lunisolar calendar, guiding agricultural activities, ritual ceremonies, and the timing of festivals.\n\nThe concept of the Three Enclosures (三垣, San Yuan), which include the Purple Forbidden enclosure (centered around the North Star), the Supreme Palace enclosure, and the Heavenly Market enclosure, surrounds the North celestial pole and comprises some of the most prominent constellations observed by Chinese astronomers. These enclosures contain stars and constellations that were of particular importance for imperial astrology and governance.\n\nChinese constellations are not just significant for their role in timekeeping and agriculture; they also have profound cultural and mythological implications. Many stories and legends are associated with the stars, and these tales have been integrated into Chinese literature, art, and philosophy over millennia.\n\nThroughout its history, Chinese astronomy has made significant contributions to the global understanding of the cosmos, including detailed star maps, the discovery of supernovae, and the development of a sophisticated calendar system that intricately links lunar phases with solar terms.";
        }
        string[] dataset;
        if (datasetMap.TryGetValue(SelectedConstellationDataset, out dataset))
        {
            return dataset;
        }
        else
        {
            Debug.LogError("Selected constellation dataset not found in dataset map: " + SelectedConstellationDataset);
            return null;
        }
    }


}
