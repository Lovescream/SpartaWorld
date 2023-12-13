# SpartaWorld



[구현 기능]  
# 데이터 관리  
- RawData를 json으로 파싱 후 DataManager를 통해 데이터를 관리함.
- Level, Item, Character 정보를 데이터로 관리함.

# 플레이어  
- 레벨과 경험치 등을 가지고 있음.
- 체력과 공격력, 방어력, 치명타 확률은 Stat이라는 별도의 클래스로 만들고 Status를 통해 관리.
- 인벤토리를 가지고 있음.

# 인벤토리와 아이템
- 아이템은 ItemData를 통해 생성할 수 있고, 아이템 목록을 관리하는 Inventory 클래스가 있음.
- 플레이어와 상점은 Inventory를 상속하는 전용 인벤토리를 가짐.
- 아이템을 장착/장착해제하면 능력치 변화가 일어나도록 함.

# UI
- SceneUI, PopupUI, DrawerUI, ToastUI로 나누어 구현.
- 캐릭터 정보, 인벤토리, 상점 등을 나타내는 UI를 구현.
- 이벤트를 통해 정보가 변경되면 UI도 변경되게끔 구현.
