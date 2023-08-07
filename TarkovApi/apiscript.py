import time
import json
from io import open

item_types = {"57864ee62459775490116fc1": ["BarterItem", "Battery"], "57864ada245977548638de91": ["BarterItem", "BuildingMaterial"], "57864a66245977548f04a81f": ["BarterItem", "Electronics"], "57864c322459775490116fbf": ["BarterItem", "HouseholdGoods"], "57864a3d24597754843f8721": ["BarterItem", "Jewelry"], "5d650c3e815116009f6201d2": ["Lubricant", "Fuel"], "57864e4c24597754843f8723": ["BarterItem", "Lubricant"], "57864c8c245977548867e7f1": ["BarterItem", "MedicalSupplies"], "590c745b86f7743cc433c5f2": ["BarterItem", "Other"], "57864bb7245977548b3b66c2": ["BarterItem", "Tool"], "5448eb774bdc2d0a728b4567": ["Item", "BarterItem"], "55d720f24bdc2d88028b456d": ["CompoundItem", "Inventory"], "55818afb4bdc2dde698b456d": ["FunctionalMod", "Bipod"], "55818af64bdc2d5b648b4570": ["FunctionalMod", "Foregrip"], "5671435f4bdc2d96058b4569": ["CompoundItem", "LockableContainer"], "55818b084bdc2d5b648b4571": ["FunctionalMod", "Flashlight"], "56ea9461d2720b67698b456f": ["FunctionalMod", "Gasblock"], "550aa4bf4bdc2dd6348b456b": ["Muzzle", "FlashHider"], "550aa4dd4bdc2dc9348b4569": ["Muzzle", "MuzzleCombo"], "550aa4cd4bdc2dd8348b456c": ["Muzzle", "Silencer"], "5448fe394bdc2d0d028b456c": ["FunctionalMod", "Muzzle"], "55818add4bdc2d5b648b456f": ["Sights", "AssaultScope"], "55818ad54bdc2ddc698b4569": ["Sights", "Collimator"], "55818acf4bdc2dde698b456b": ["Sights", "CompactCollimator"], "55818ac54bdc2d5b648b456e": ["Sights", "IronSight"], "55818ae44bdc2dde698b456c": ["Sights", "OpticScope"], "5a2c3a9486f774688b05e574": ["SpecialScope", "NightVision"], "55818aeb4bdc2ddc698b456a": ["Sights", "SpecialScope"], "5d21f59b6dbe99052b54ef83": ["SpecialScope", "ThermalVision"], "5448fe7a4bdc2d6f028b456b": ["FunctionalMod", "Sights"], "55818b164bdc2ddc698b456c": ["FunctionalMod", "TacticalCombo"], "5a74651486f7744e73386dd1": ["FunctionalMod", "AuxiliaryMod"], "550aa4154bdc2dd8348b456b": ["Mod", "FunctionalMod"], "55818a6f4bdc2db9688b456b": ["GearMod", "Charge"], "5448bc234bdc2d3c308b4569": ["GearMod", "Magazine"], "610720f290b75a49ff2e5e25": ["Magazine", "CylinderMagazine"], "55818b224bdc2dde698b456f": ["GearMod", "Mount"], "55818a594bdc2db9688b456a": ["GearMod", "Stock"], "55802f3e4bdc2de7118b4584": ["Mod", "GearMod"], "555ef6e44bdc2de9068b457e": ["MasterMod", "Barrel"], "55818a104bdc2db9688b4569": ["MasterMod", "Handguard"], "55818a684bdc2ddd698b456d": ["MasterMod", "PistolGrip"], "55818a304bdc2db5418b457d": ["MasterMod", "Receiver"], "55802f4a4bdc2ddb688b4569": ["Mod", "MasterMod"], "5448fe124bdc2da5018b4567": ["CompoundItem", "Mod"], "5448e53e4bdc2d60728b4567": ["SearchableItem", "Backpack"], "566965d44bdc2d814c8b4571": ["SearchableItem", "LootContainer"], "5448bf274bdc2dfc2f8b456a": ["SearchableItem", "MobContainer"], "557596e64bdc2dc2118b4571": ["SearchableItem", "Pockets"], "5448e5284bdc2dcb718b4567": ["SearchableItem", "Vest"], "566168634bdc2d144c8b456c": ["CompoundItem", "SearchableItem"], "5795f317245977243854e041": ["CompoundItem", "SimpleContainer"], "566abbb64bdc2d144c8b457d": ["CompoundItem", "Stash"], "6050cac987d3f925bf016837": ["Stash", "SortingTable"], "5447b5fc4bdc2d87278b4567": ["Weapon", "AssaultCarbine"], "5447b5f14bdc2d61278b4567": ["Weapon", "AssaultRifle"], "5447b6194bdc2d67278b4567": ["Weapon", "MarksmanRifle"], "5447b5cf4bdc2d65278b4567": ["Weapon", "Pistol"], "5447b6094bdc2dc3278b4567": ["Weapon", "Shotgun"], "5447b5e04bdc2d62278b4567": ["Weapon", "Smg"], "5447b6254bdc2dc3278b4568": ["Weapon", "SniperRifle"], "5447bed64bdc2d97278b4568": ["Weapon", "MachineGun"], "5447bedf4bdc2d87278b4568": ["Weapon", "GrenadeLauncher"], "617f1ef5e8b54b0998387733": ["Weapon", "Revolver"], "5422acb9af1c889c16000029": ["CompoundItem", "Weapon"], "5b3f15d486f77432d0509248": ["Equipment", "ArmBand"], "5448e54d4bdc2dcc718b4568": ["ArmoredEquipment", "Armor"], "5a341c4686f77469e155819e": ["ArmoredEquipment", "FaceCover"], "5a341c4086f77401f2541505": ["ArmoredEquipment", "Headwear"], "5448e5724bdc2ddf718b4568": ["ArmoredEquipment", "Visors"], "57bef4c42459772e8d35a53b": ["Equipment", "ArmoredEquipment"], "5645bcb74bdc2ded0b8b4578": ["Equipment", "Headphones"], "543be5f84bdc2dd4348b456a": ["CompoundItem", "Equipment"], "566162e44bdc2d3f298b4573": ["Item", "CompoundItem"], "5448e8d64bdc2dce718b4568": ["FoodDrink", "Drink"], "5448e8d04bdc2ddf718b4569": ["FoodDrink", "Food"], "543be6674bdc2df1348b4569": ["Item", "FoodDrink"], "5448ecbe4bdc2d60728b4568": ["Item", "Info"], "5c164d2286f774194c5e69fa": ["Key", "Keycard"], "5c99f98d86f7745c314214b3": ["Key", "KeyMechanical"], "543be5e94bdc2df1348b4568": ["Item", "Key"], "5447e1d04bdc2dff2f8b4567": ["Item", "Knife"], "567849dd4bdc2d150f8b456e": ["Item", "Map"], "5448f3a14bdc2d27728b4569": ["Meds", "Drugs"], "5448f39d4bdc2d0a728b4568": ["Meds", "MedKit"], "5448f3ac4bdc2dce718b4569": ["Meds", "Medical"], "5448f3a64bdc2d60728b456a": ["Meds", "Stimulator"], "543be5664bdc2dd4348b4569": ["Item", "Meds"], "5447e0e74bdc2d3c308b4567": ["Item", "SpecItem"], "61605ddea09d851a0a0c1bbc": ["SpecItem", "PortableRangeFinder"], "5f4fbaaca5573a5ac31db429": ["SpecItem", "Compass"], "5485a8684bdc2da71d8b4567": ["StackableItem", "Ammo"], "543be5cb4bdc2deb348b4568": ["StackableItem", "AmmoBox"], "543be5dd4bdc2deb348b4569": ["StackableItem", "Money"], "5661632d4bdc2d903d8b456b": ["Item", "StackableItem"], "543be6564bdc2df4348b4568": ["Item", "ThrowWeap"], "616eb7aea207f41933308f46": ["RepairKits", "RepairKit"], "55818b014bdc2ddc698b456b": ["Weapon", "GrenadeLauncher"], "627a137bf21bc425b06ab944": ["Weapon", "GrenadeLauncher"],"62f109593b54472778797866": ["SearchableItem", "EventContainer"]}

raw_bsg_json = open('bsg_raw.json' , encoding='UTF-8')
tm_list_json = open('tarkov-market-all.json' , encoding='UTF-8')

raw_bsg = json.load(raw_bsg_json)
tm_list = json.load(tm_list_json)

for hash in raw_bsg:
    item = raw_bsg.get(hash)
    
    if item:
        
        item_cat = ""
        
        props = item.get('_props')
        parent = item.get('_parent')
        
        if parent:
            try:
                item_main_type = "item_type." + item_types[parent][0];
                item_sub_type = "item_subtype." + item_types[parent][1];
            except:
                item_main_type = "item_type.unknown"
                item_sub_type = "item_subtype.unknown"
            
            if props:
                name  = props.get('ShortName')
                u_name = item.get('_name')
                
                slot_width = props.get('Width')
                slot_height = props.get('Height')

                for i in tm_list:
                   if i['bsgId'] == hash:
                        name  = i['shortName']
                        u_name = item.get('name')
                        price = 0
                        price = i['avg24hPrice']
                        if price == 0:
                            price_per_slot = 0
                        else:
                            price_per_slot = int(price / (slot_height*slot_width))                  
                
                if name == "Item":
                    continue
                    
                try:
                    name = name.replace('"', "'")
                except:
                    name = name
                
                id = item.get('_id')
                print(f"{{ \"{id}\", new OurItem ( \"{name}\", {price}, {price_per_slot}, {item_main_type}, {item_sub_type} ) }},")
                i = 0
