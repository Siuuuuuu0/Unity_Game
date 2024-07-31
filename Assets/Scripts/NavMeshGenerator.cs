using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using NavMeshPlus.Components;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using NavMeshSurface = NavMeshPlus.Components.NavMeshSurface;


public class NavMeshGenerator : MonoBehaviour
{
    public GameObject enemy;
    
    public void Generate(){
        //GetComponent<NavMeshSurface>().navMeshData=null;
        GetComponent<NavMeshSurface>().navMeshData=null;
        GetComponent<NavMeshSurface>().BuildNavMesh();
        //GameObject enemy = new GameObject("Enemy");
        // Vector3 sourcePostion = new Vector3( 19.5f, 19.5f, 0);//The position you want to place your agent
        // NavMeshHit closestHit;
        // if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas))
        //     gameObject.transform.position = closestHit.position;
        // else
        //     Debug.LogError("Could not find position on NavMesh!");
        
        Instantiate(enemy, new Vector3(59, 59, 0), Quaternion.identity);
        // if( NavMesh.SamplePosition(  sourcePostion, out closestHit, 500, 1 ) ){
        //     enemy.transform.position = closestHit.position;
        //     enemy.AddComponent<NavMeshAgent>();
            
        // }
        //Instantiate(enemy,  new Vector3(5, -2, 0),  Quaternion.identity);
        //Instantiate(enemy, new Vector3(19.5f, 19.5f, 0), Quaternion.identity);
        
    }
    // public void GenerateAsync(Bounds bounds){
    //     // GetComponent<NavMeshSurface>().BuildNavMeshAsync();
    //     NavMeshData data =  
    //         NavMeshBuilder.BuildNavMeshData(NavMesh.GetSettingsByID(0), new List<NavMeshBuildSource>{GetComponent<NavMeshSurface>().}, bounds, transform.position, transform.rotation);
    //     //NavMeshBuildSettings.Preserve
    // }
    // public void UpdateNavMeshWithinBounds(Bounds bounds)
    // {
    //     // Find all NavMeshBuildSource objects within the specified bounds
    //     NavMeshBuildSource[] sources = NavMeshBuilder.CollectSources();
        

    //     // Update the navmesh using the collected sources
    //     UpdateNavMesh(bounds);
    // }

    // // NavMeshBuildSource[] CollectSourcesWithinBounds(Bounds bounds)
    // // {
    // //     // Find all NavMeshBuildSource objects within the specified bounds
    // //     NavMeshBuildSettings buildSettings = NavMesh.GetSettingsByID(0); // Use the default settings (adjust ID as needed)
    // //     return NavMeshBuilder.CollectSources(bounds, buildSettings);
    // // }

    // void UpdateNavMesh(Bounds bounds)
    // {
    //     // Create a NavMeshBuildSettings object with your desired settings
    //     NavMeshBuildSettings buildSettings = NavMesh.GetSettingsByID(0); // Use the default settings (adjust ID as needed)

    //     // Build the navmesh using the collected sources and build settings
    //     NavMeshData data= NavMeshBuilder.BuildNavMeshData(buildSettings, null, bounds, transform.position, transform.rotation);
    // }
    // public void UpdateNavMeshWithinBounds(Bounds bounds)
    // {
    //     // Define the parameters for NavMesh.CollectSources
    //     int includedLayerMask = NavMesh.AllAreas; // Include all layers, adjust as needed
    //     NavMeshCollectGeometry geometry = NavMeshCollectGeometry.RenderMeshes; // Adjust as needed
    //     int defaultArea = 0; // Adjust the default area as needed
    //     List<NavMeshBuildMarkup> markups = new List<NavMeshBuildMarkup>(); // You can add markup if needed
    //     List<NavMeshBuildSource> results = new List<NavMeshBuildSource>();

    //     // Collect sources within the specified bounds
    //     NavMeshBuilder.CollectSources(bounds, includedLayerMask, geometry, defaultArea, markups, results);
        
    //     // Update the navmesh using the collected sources
    //     UpdateNavMesh(results, bounds);
    // }

    // void UpdateNavMesh(List<NavMeshBuildSource> sources, Bounds bounds)
    // {
    //     // Create a NavMeshBuildSettings object with your desired settings
    //     NavMeshBuildSettings buildSettings = NavMesh.GetSettingsByID(0); // Use the default settings (adjust ID as needed)
        
    //     // Build the navmesh using the collected sources and build settings
    //     NavMeshData data = NavMeshBuilder.BuildNavMeshData(buildSettings, sources, bounds, transform.position, transform.rotation);
    //     //NavMeshBuilder.UpdateNavMeshDataAsync(data, buildSettings, bounds);
    //     GetComponent<NavMeshData>()
    // }

}
