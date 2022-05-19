using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    [SerializeField] private Dropdown _hairBaseDropdown;
    [SerializeField] private Dropdown _hairFrontDropdown;
    [SerializeField] private Dropdown _accessoryDropdown;
    [SerializeField] private Dropdown _faceDropdown;
    [SerializeField] private Dropdown _costumeDropdown;
    [SerializeField] private Dropdown _hairMatDropdown;

    [SerializeField] private List<GameObject> _hairBaseList;
    [SerializeField] private List<GameObject> _hairFrontList;
    [SerializeField] private List<GameObject> _accessoryList;
    [SerializeField] private List<GameObject> _faceList;
    [SerializeField] private List<GameObject> _costumeList;

    private List<Material> hairFrontMatList = new List<Material>();
    void Start(){
        _hairBaseDropdown.onValueChanged.AddListener(value =>
        {
            _hairBaseList.ForEach(obj => obj.SetActive(false));
            foreach (var t in _hairBaseList)
            {
                if (t.name == _hairBaseDropdown.options[value].text)
                {
                    t.gameObject.SetActive(true);
                }
            }
        });
        
        _hairFrontDropdown.onValueChanged.AddListener(value =>
        {
            _hairFrontList.ForEach(obj => obj.SetActive(false));
            foreach (var t in _hairFrontList)
            {
                hairFrontMatList = FindAllAsset<Material>("Assets/SD Unity-Chan Haon Custom/Materials/Hair/Hair_0" + value).ToList();
                _hairMatDropdown.options = hairFrontMatList.Select(mat => new Dropdown.OptionData{text = mat.name}).ToList();
                if (t.name == _hairFrontDropdown.options[value].text)
                {
                    t.gameObject.SetActive(true);
                }
            }
        });
        
        _accessoryDropdown.onValueChanged.AddListener(value =>
        {
            _accessoryList.ForEach(obj => obj.SetActive(false));
            foreach (var t in _accessoryList)
            {
                if (t.name == _accessoryDropdown.options[value].text)
                {
                    t.gameObject.SetActive(true);
                }
            }
        });
        
        _faceDropdown.onValueChanged.AddListener(value =>
        {
            _faceList.ForEach(obj => obj.SetActive(false));
            foreach (var t in _faceList)
            {
                if (t.name == _faceDropdown.options[value].text)
                {
                    t.gameObject.SetActive(true);
                }
            }
        });
        
        _costumeList[0].gameObject.SetActive(true);
        _costumeDropdown.onValueChanged.AddListener(value =>
        {
            _costumeList.ForEach(obj => obj.SetActive(false));
            foreach (var t in _costumeList)
            {
                if (t.name == _costumeDropdown.options[value].text)
                {
                    t.gameObject.SetActive(true);
                }
            }
        });

        hairFrontMatList = FindAllAsset<Material>("Assets/SD Unity-Chan Haon Custom/Materials/Hair/Hair_00").ToList();
        _hairMatDropdown.options = hairFrontMatList.Select(mat => new Dropdown.OptionData{text = mat.name}).ToList();
        _hairMatDropdown.onValueChanged.AddListener(value =>
        {
            _hairFrontList.ForEach(obj =>
            {
                obj.GetComponentInChildren<SkinnedMeshRenderer>().material = hairFrontMatList[value];
            });
        });
    }
    
    IReadOnlyList<T> FindAllAsset<T>(string directoryPath) where T : UnityEngine.Object
    {
        List<T> assets = new List<T>();
        var fileNames = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

        foreach (var fileName in fileNames)
        {
            var asset = AssetDatabase.LoadAssetAtPath<T>(fileName);
            if (asset != null)
            {
                assets.Add(asset);
            }
        }

        return assets;
    }
}
