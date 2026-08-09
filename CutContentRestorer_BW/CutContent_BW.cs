using MelonLoader;
using ModThatIsNotMod.BoneMenu;
using StressLevelZero.Arena;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[assembly: MelonInfo(typeof(CutContentRestorer_BW.CutContent_BW), "CutContentRestorer_BW", "1.2.1", "Digit4lSpace", null)]
[assembly: MelonGame("Stress Level Zero", "BONEWORKS")]

namespace CutContentRestorer_BW
{
    public class CutContent_BW : MelonMod
    {
        private MelonPreferences_Category _category;
        private MelonPreferences_Entry<bool> _gunStuff;
        private MelonPreferences_Entry<bool> _blankBox;
        private MelonPreferences_Entry<bool> _fantasyArena;
        private MelonPreferences_Entry<bool> _throneRoomExitPortal;
        private MelonPreferences_Entry<bool> _backpackStraps;
        private MelonPreferences_Entry<bool> _NPCFordMeshes;
        private MelonPreferences_Entry<bool> _onlyNewGeoMovers;

        private Dictionary<string, (string[] enable, string[] disable)> _sceneObjects;

        private string[] _generalGunObjects =
        {
            "highlight_boxy", "boxy top", "EjectText",
            "FireModeText", "ControllerOptionSelect", "Border", "Cursor"
        };

        private string[] _backpack =
        {
            "brett_backpack_straps"
        };

        private string[] _hair =
        {
            "brett_hairCap", "brett_hairCards", "brett_accessories_belt_mesh"
        };

        public override void OnApplicationStart()
        {
            _category = MelonPreferences.CreateCategory("CutContentRestorer");
            _gunStuff = _category.CreateEntry("GunStuff", true, "Enable gun stuff");
            _blankBox = _category.CreateEntry("BlankBox", true, "Enable BlankBox stuff");
            _fantasyArena = _category.CreateEntry("FantasyArena", true, "Enable Fantasy Arena stuff");
            _throneRoomExitPortal = _category.CreateEntry("ThroneRoomExitPortal", true, "Enable Throne Room Exit Portal");
            _backpackStraps = _category.CreateEntry("BackpackStraps", true, "Enable the Backpack Straps");
            _NPCFordMeshes = _category.CreateEntry("NPCFordMeshes", true, "Restore Ford meshes on most NPCs");
            _onlyNewGeoMovers = _category.CreateEntry("DebugOnlyNewGeoMovers", false, "bloop");
            MelonPreferences.Save();

            var menu = MenuManager.CreateCategory("Cut Content Restorer", Color.magenta);

            menu.CreateBoolElement("Gun Stuff", Color.white, _gunStuff.Value,
                (bool v) => { _gunStuff.Value = v; MelonPreferences.Save(); });

            menu.CreateBoolElement("Blank Box", Color.white, _blankBox.Value,
                (bool v) => { _blankBox.Value = v; MelonPreferences.Save(); });

            menu.CreateBoolElement("Fantasy Arena", Color.white, _fantasyArena.Value,
                (bool v) => { _fantasyArena.Value = v; MelonPreferences.Save(); });

            menu.CreateBoolElement("Throne Room Exit Portal", Color.white, _throneRoomExitPortal.Value,
                (bool v) => { _throneRoomExitPortal.Value = v; MelonPreferences.Save(); });

            menu.CreateBoolElement("Backpack Straps", Color.white, _backpackStraps.Value,
                (bool v) => { _backpackStraps.Value = v; MelonPreferences.Save(); });

            menu.CreateBoolElement("NPC Ford Meshes", Color.white, _NPCFordMeshes.Value,
                (bool v) => { _NPCFordMeshes.Value = v; MelonPreferences.Save(); });

            menu.CreateBoolElement("Only New Geo Movers", Color.white, _onlyNewGeoMovers.Value,
                (bool v) => { _onlyNewGeoMovers.Value = v; MelonPreferences.Save(); });

            _sceneObjects = new Dictionary<string, (string[], string[])>
            {
                ["scene_introStart"] = (
                    new[] { "Measurement Device", "panel_global", "button_Zombies", "button_DebugLines" },
                    new string[] { }
                ),
                ["scene_mainMenu"] = (
                    new[] { "prop_bigButton", "button_updatesNext", "button_updatesBack", "grid_Window",
                            "button_PROFILE", "LIGHTBAKEOFF", "CLAMP_greyBox", "prop_LEVELMODULE_greybox",
                            "BASICDESKSETUP", "holo_Sidearm_PT8_Alaris", "D_Radio", "Cart" },
                    new string[] { }
                ),

                // Campaign


                ["scene_breakroom"] = (
                    new[] { "Particle System", "plane_4x5 (1)", "wall_curver_4m_convex (1)", "wall_curver_4m_convex (2)",
                            "wall_curver_4m_convex (4)", "wall_curver_4m_convex (7)", "model_onOffLever_light_A",
                            "model_onOffLever_light_B", "model_onOffLever_lightHousing", "Spot Light", "OFF",
                            "CLAWTRACK", "boss_CLAW", "LIGHTBAKEOFF", "SKYBOX", "hallway_piece_4mx20m (2)", "plane_4x16 (2)",
                            "hallway_piece_4mx80m", "plane_4x18", "plane_4x16 (3)", "ramp_1x2x1_stairs (2)", "ramp_1x2x1_stairs (3)",
                            "Cylinder (4)", "Cylinder (5)", "Cylinder (6)", "Cylinder (7)", "Cylinder (8)", "Cylinder (9)", "Cylinder (10)",
                            "plane (11)", "plane_12x20 (4)", "plane_12x20 (5)", "plane_12x20 (19)", "plane_12x20 (15)", "plane_12x20 (18)" },
                    new[] { "hallway_piece_4mx20m", "hallway_piece_4mx20m (1)" }
                ),
                ["scene_museum"] = (
                    new[] { "plateCover_top_null", "prop_onOffSlider", "PIPECLIMBER", "poster_monogon_A",
                            "poster_monogon_B", "poster_monogon_C", "poster_monogon_D", "poster_monogon_E",
                            "poster_monogon_F", "poster_monogon_G", "poster_monogon_H", "poster_monogon_J",
                            "poster_monogon_K", "ChristmasTree", "holoRing_Text", "Particle System" },
                    new string[] { }
                ),
                ["scene_streets"] = (
                    new[] { "CombineLock", "plateCover_top_null", "prop_onOffSlider", "holoRing_Text", "Particle System" },
                    new string[] { }
                ),
                ["scene_runoff"] = (
                    new[] { "skele_Geo", "plateCover_top_null", "prop_onOffSlider", "holoRing_Text", "Particle System" },
                    new string[] { }
                ),
                ["scene_sewerStation"] = (
                    new[] { "plateCover_top_null", "prop_onOffSlider", "Pipes", "holoRing_Text", "Particle System" },
                    new string[] { }
                ),
                ["scene_warehouse"] = (
                    new[] { "FireSafetyDoor01", "ROOM_EXIT", "plateCover_top_null", "prop_onOffSlider", "Particle System" },
                    new[] { "plane_4x4 (4)", "plane_4x4 (1)" }
                ),
                ["scene_subwayStation"] = (
                    new[] { "plateCover_top_null", "prop_onOffSlider", "holoRing_Text", "Particle System" },
                    new string[] { }
                ),
                ["scene_tower"] = (
                    new[] { "plateCover_top_null", "prop_onOffSlider", "holoRing_Text", "Particle System" },
                    new string[] { }
                ),
                ["scene_towerBoss"] = (
                    new[] { "skele_Geo", "VoiD Leak Stage (1)", "grid_30m_ballistic",
                            "STAIRS (1)", "plateCover_top_null", "prop_onOffSlider", "Particle System" },
                    new string[] { }
                ),
                ["scene_arena"] = (
                    new[] { "OLD_SECTION_WINDMILL-HOUSES", "grid_Castle (1)", "CARGOCART (1)", "CARGOCART",
                            "ARCHBLOCK (36)", "OCTAGON", "concretebarrier_turbo", "Ifonche_02", "Ifonche_01",
                            "WoodF2", "dest_BoneworksCrate_1m", "kitbash_smallCons_16stair", "WoodG2 (24)",
                            "JamaRocks", "crate_military_white_ARENA Variant", "Tawern Interior", "BrettEnemy", "Particle System" },
                    new[] { "mesh_kitbash_smallCons_16stair-1729422 (1)", "Door001" }
                ),
                ["scene_throneRoom"] = (
                    new[] { "chamber_42x42x42_r", "texter_slzLogo", "TRIGGER_EXIT", "punching_bag", "skele_Geo",
                            "handle", "handle (2)", "kitbash_platform_4m_4m", "stair_metal_10",
                            "stair_metal_10 (2)", "stair_metal_10 (1)", "stair_metal_10 (3)", "Particle System" },
                    new[] { "texter_02", "texter_Chamber", "ELEVATORCART", "pillar_quarterxquarterx4 (1)" }
                ),

                // Arena

                ["arena_fantasy"] = (
                    new[] { "WeaponSpawn", "MagSpawn", "BALLOON_Revolver", "prop_ammoBox_hvy", "dest_BoneworksCrate_1m",
                            "Ifonche_02", "Ifonche_01", "WoodF2", "kitbash_smallCons_16stair" },
                    new[] { "mesh_kitbash_smallCons_16stair-1729422 (1)", "Cylinder", "Cylinder_1", "Cylinder_2",
                             "Cylinder_3", "Cylinder_4" }
                ),
                ["zombie_warehouse"] = (
                    new[] { "plateCover_top_null", "prop_onOffSlider", "image_Action", "prop_arenaBell (2)", "dest_plant_C",
                            "ZipStick", "ZipStick (1)", "SAVESPOT*REMOVE THIS*ZONEDEBUG", "holoRing_Text", "Particle System" },
                    new[] { "red_couch", "CUREMACHINE (1)" }
                ),

                // Sandbox

                ["sandbox_museumBasement"] = (
                    new[] { "HALFPIPE", "chamber_piece_40mx40m (21)", "chamber_piece_40mx40m (22)",
                            "prop_batteryHolderA", "prop_batteryHolderA (1)", "prop_batteryHolderA (2)",
                            "prop_batteryA", "prop_batteryA (1)", "prop_batteryA (2)", "GravityStaff",
                            "Balloon Spawn Launcher", "Decal_GroundMarking_Circle", "p_dest_glassPane (5)",
                            "p_dest_glassPane (1)", "p_dest_glassPane (2)", "p_dest_glassPane (4)",
                            "p_dest_glassPane (3)", "testiKey", "CONCRETEPALLETE", "ZoneDebugger",
                            "rifle_M16_Ironsights", "Knife_Chef", "env_monomat_dispSys", "COREBOMB",
                            "DEVMANIPTOOL", "prop_crown", "prop_powerPuncher (1)", "prop_powerPuncher",
                            "rifle_M16_LaserForegrip" },
                    new string[] { }
                ),
                ["sandbox_blankBox"] = (
                    new[] { "HALFPIPE", "chamber_piece_40mx40m (21)", "chamber_piece_40mx40m (22)",
                            "prop_batteryHolderA", "prop_batteryHolderA (1)", "prop_batteryHolderA (2)",
                            "prop_batteryA", "prop_batteryA (1)", "prop_batteryA (2)", "GravityStaff",
                            "Balloon Spawn Launcher", "Decal_GroundMarking_Circle", "p_dest_glassPane (5)",
                            "p_dest_glassPane (1)", "p_dest_glassPane (2)", "p_dest_glassPane (4)",
                            "p_dest_glassPane (3)", "testiKey", "CONCRETEPALLETE", "ZoneDebugger",
                            "rifle_M16_Ironsights", "Knife_Chef", "env_monomat_dispSys", "COREBOMB",
                            "DEVMANIPTOOL", "prop_crown", "prop_powerPuncher (1)", "prop_powerPuncher",
                            "rifle_M16_LaserForegrip", "Sacffolding" },
                    new string[] { }
                ),
                ["scene_redactedChamber"] = (
                    new[] { "texter_04", "texter_awareness", "texter_body", "texter_weapons", "texter_testing",
                            "place_room", "STAIRS (1)", "DECALS_RECURSION1", "place_deskStation",
                            "-------HIDDEN-------", "lamp_worklight_stand", "confetti_Poof_ball", "LevelSelector",
                            "LevelPillar", "enemy_roller", "QuickMenu", "TimeMachine", "prop_BigLever",
                            "SM_Chair1", "prop_onOffLever", "floor", "Construction", "UI", "electronics", "Particle System" },
                    new[] { "texter_TestChamber", "texter_interaction", "texter_physical", "dest_speaker" }
                ),
                ["scene_Tuscany"] = (
                    new[] { "BIGTARGET", "BIGTARGET (1)", "BIGTARGET (2)", "BIGTARGET (3)", "Boats", "Boat3",
                            "Tapestry", "Books", "book_B_wormholes", "book_C_livingStressFree", "book_L_MakeGames",
                            "book_I_texturingForMorons" },
                    new string[] { }
                ),
                ["scene_hoverJunkers"] = (
                    new[] { "Television", "holoRing_Text" },
                    new string[] { }
                ),
                ["sandbox_handgunBox"] = (
                    new[] { "target", "testiKey", "prop_batteryA (1)", "prop_batteryA (2)", "prop_batteryHolderA (1)",
                            "prop_batteryHolderA (2)", "punching_bag", "grip_Pole", "txt_Time", "plateCover_top_null", "prop_onOffSlider", "Particle System" },
                    new string[] { }
                ),
            };
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            MelonCoroutines.Start(HandleScene(sceneName));

            if (sceneName == "arena_fantasy")
                MelonCoroutines.Start(ArenaSwitcher());
        }

        private void ArenaCampaignStuff(GameObject[] allObjects)
        {
            GameObject floorCollider = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floorCollider.transform.position = new Vector3(1.568f, -0.003f, 120.2837f);
            floorCollider.transform.rotation = Quaternion.Euler(-90.00001f, 0f, -90.00001f);
            floorCollider.transform.localScale = new Vector3(10.8033f, 10.80591f, 0.03490543f);
            floorCollider.GetComponent<MeshRenderer>().enabled = false;
            LoggerInstance.Msg("Tavern floor Void clip fixed!");

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "TRIGGER_EXIT" && obj.scene.name == "scene_arena")
                {
                    obj.transform.position = new Vector3(41.91f, -0.86f, 131.99f);
                    obj.transform.rotation = Quaternion.Euler(180f, 89.99999f, 180f);
                    LoggerInstance.Msg("Arena portal moved!");
                }
            }
        }

        private void ZombieWarehouseStuff(GameObject[] allObjects)
        {
            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "prop_onOffLever (1)" && obj.scene.name == "zombie_warehouse" && obj.transform.parent.name == "CUREMACHINE (1)")
                {
                    foreach (GameObject target in allObjects)
                    {
                        if (target.name == "OLD" && target.scene.name == "zombie_warehouse")
                        {
                            obj.transform.SetParent(target.transform);
                            obj.transform.localPosition = new Vector3(-0.074f, 0.289f, 0.564f);
                            obj.SetActive(true);
                        }
                    }
                }
            }

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "OLD" && obj.scene.name == "zombie_warehouse")
                {
                    obj.SetActive(true);
                    for (int i = 0; i < obj.transform.childCount; i++)
                    {
                        obj.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    LoggerInstance.Msg("Old Cure Machine enabled!");
                }
            }
        }

        private void RedactedChamberStuff(GameObject[] allObjects)
        {
            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "dest_explodingBarrel" && obj.scene.name == "scene_redactedChamber")
                {
                    obj.transform.position = new Vector3(-11.453f, -0.86f, 2.492f);
                    obj.transform.rotation = Quaternion.Euler(0f, -135.393f, 0f);
                }
                if (obj.name == "dest_pallet_A" && obj.scene.name == "scene_redactedChamber")
                {
                    obj.transform.position = new Vector3(-10.896f, -0.86f, -1.656f);
                }
                if (obj.name == "Container (1)" && obj.scene.name == "scene_redactedChamber")
                {
                    obj.transform.position = new Vector3(-8.89f, 2.65f, -6.99f);
                }
                if (obj.name == "Hexagonal_Container" && obj.scene.name == "scene_redactedChamber")
                {
                    obj.transform.position = new Vector3(-8.89f, 0.9f, -6.99f);
                }
                if (obj.name == "place_deskStation" && obj.scene.name == "scene_redactedChamber")
                {
                    obj.transform.position = new Vector3(-1.306f, 1.002f, 19.007f);
                }
            }
        }

        private void MuseumBasementStuff(GameObject[] allObjects)
        {
            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "env_monomat_dispSys" && obj.scene.name == "sandbox_museumBasement")
                {
                    obj.transform.position = new Vector3(3.88f, 0f, -0.507f);
                }
            }
        }

        private IEnumerator ArenaSwitcher()
        {
            yield return new WaitForSeconds(1f);

            GameObject swappable = GameObject.Find("SwappableArenaObstacles");
            if (swappable == null)
            {
                LoggerInstance.Warning("SwappableArenaObstacles not found!");
                yield break;
            }

            Arena_GeoManager manager = swappable.GetComponent<Arena_GeoManager>();
            if (manager == null)
            {
                LoggerInstance.Warning("Arena_GeoManager not found!");
                yield break;
            }

            LoggerInstance.Msg($"Found Arena_GeoManager, geoMoverList has {manager.geoMoverList.Count} entries.");

            if (_onlyNewGeoMovers.Value)
            {
                manager.geoMoverList.Clear();
                LoggerInstance.Msg("Cleared original geoMoverList.");
            }

            string[] toAdd = { "TriangularProtect", "Crabsketball" };

            foreach (string name in toAdd)
            {
                Arena_GeoMover mover = null;
                foreach (Arena_GeoMover m in Resources.FindObjectsOfTypeAll<Arena_GeoMover>())
                {
                    if (m.gameObject.name == name)
                    {
                        mover = m;
                        break;
                    }
                }

                if (mover == null)
                {
                    LoggerInstance.Warning($"Could not find Arena_GeoMover: {name}");
                    continue;
                } // ur not supposed to see this

                manager.geoMoverList.Add(mover);
                LoggerInstance.Msg($"Added {name} to geoMoverList.");
            }

            LoggerInstance.Msg($"geoMoverList now has {manager.geoMoverList.Count} entries.");
        }

        private IEnumerator HandleScene(string sceneName)
        {
            yield return new WaitForSeconds(1f);

            GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                if (_gunStuff.Value && _generalGunObjects.Contains(obj.name))
                    obj.SetActive(true);

                if (_backpackStraps.Value && _backpack.Contains(obj.name))
                    obj.SetActive(true);

                if (_NPCFordMeshes.Value && _hair.Contains(obj.name))
                    obj.SetActive(true);
            }

            if (!_sceneObjects.TryGetValue(sceneName, out var data)) yield break;

            if (sceneName == "sandbox_blankBox" && !_blankBox.Value || sceneName == "arena_fantasy" && !_fantasyArena.Value) yield break;

            if (sceneName == "scene_arena")
                ArenaCampaignStuff(allObjects);

            if (sceneName == "zombie_warehouse")
                ZombieWarehouseStuff(allObjects);

            if (sceneName == "scene_redactedChamber")
                RedactedChamberStuff(allObjects);

            if (sceneName == "sandbox_museumBasement")
                MuseumBasementStuff(allObjects);

            int count = 0;

            foreach (GameObject obj in allObjects)
            {
                if (obj.scene.name != sceneName) continue;

                if (data.enable.Contains(obj.name))
                {
                    obj.SetActive(true);
                    count++;
                    LoggerInstance.Msg($"{obj.name} Enabled.");
                }
                else if (data.disable.Contains(obj.name))
                {
                    obj.SetActive(false);
                }
            }

            if (sceneName == "scene_throneRoom" && !_throneRoomExitPortal.Value)
            {
                foreach (GameObject obj in allObjects)
                {
                    if (obj.name == "TRIGGER_EXIT" && obj.scene.name == "scene_throneRoom")
                        obj.SetActive(false);
                }
            }

            LoggerInstance.Msg($"Enabled {count} objects in {sceneName}!");
        }
    }
}
