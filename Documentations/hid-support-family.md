----
title: "HID Support Family"
tags: ["hid"]
relates: ["prefab:HIDExpressionManager", "script:controllers/HIDExpressionManagement", "script:models/HIDInteractionInvokerDefaults", "script:models/InteractionKeyConfig"]
----

HID 지원을 위한 코드 시리즈입니다.

## 배경

이 코드 시리즈는 커밋 및 PR 당시 마우스의 포커스 설정/해제를 지원하기 위해 작성되었습니다. 이 과정에서 포커스 모드를 전환하는 키의 입력을 감지하고, 포커스 모드의 상태를 관리하며, 전역에서 참조 가능한 관리자의 기능이 필요했습니다.  

일련의 기능을 대응하고 확장성을 갖추기 위해 모든 HID 및 입력 이벤트를 지원 가능하게 확장할 수 있도록(_다시 말해 실제로는 구현되지 않았습니다._) 이 코드를 추가하였습니다.

## 설계

### 키 이벤트 및 상태 관리

HID 이벤트로서 발생하는 상태는 `HIDExpressionManagement`가 관리합니다. 예를 들어, 포커스 모드의 최종 상태는 이 클래스가 관리하며, 다른 코드에서 이 클래스를 통해 상태를 참조할 수 있습니다.  
_이 클래스는 싱글톤 객체를 자동 생성하므로, 실제로는 싱글톤 객체를 통해 참조합니다._  
```cs
HIDExpressionManagement.instance;
```

하지만 `HIDExpressionManagement`는 상태 데이터를 관리하는 데 집중하므로, 각 상태에 따른 대응은 `HIDExpressionManager`가 수행합니다. 다시 말해 `HIDExpressionMangement`는 데이터 스토리지이고, `HIDExpressionManager`는 핸들러에 가깝습니다.

### 키 설정값 관리

`HIDExpressionManager`가 HID 입력 이벤트를 감지하는데 활용할 데이터로 `HIDInteractionInvokerConfig`, `HIDInteractionInvokerConfigHolder`가 있습니다. `HIDInteractionInvokerConfig`는 소위 말해, 키 세팅을 지원하기 위한 구조체입니다.  

하지만 `HIDInteractionInvokerConfig`는 구조체이므로, 전역적으로 참조하기 위해 `HIDInteractionConfigHolder`의 싱글톤 객체를 사용해야 합니다.

실제로는 아래와 같이 `HIDInteractionInvokerConfig`를 호출할 수 있습니다.
```cs
HIDInteractionInvokerConfigHolder.instance;
```

이와 같이 구조체와 객체를 구분함으로서, 직렬화할 키 세팅 필드의 범위를 명확하게 구분할 수 있습니다. 이후에 키 세팅 필드를 직렬화 하는 방향으로 개발이 진행된다면, `HIDInteractionInvokerConfig`만을 직렬화하면 됩니다.  
_최초 커밋 시점에서는 이미 `HIDInteractionInvokerConfig`에 `[Serializable]` 처리를 해두었습니다._

최초 커밋 시점의 코드 기준으로, `HIDInteractionInvokerConfig`의 값은 `HIDInteractionInvokerDefaults`에 의해 결정됩니다. _만약 이후에 키 세팅을 저장하고 로드할 수 있도록 개발이 진행된다면, `HIDInteractionInvokerConfig`는 세팅이 제대로 로드되지 않을 때에만 사용할 수 있도록 수정되어야 합니다._

## HIDExpressionManager 프리팹

`HIDExpressionManager` 클래스를 실제로 사용하는 방법 중 하나로서 추가하였습니다.  

_하지만 파라미터 설정이 존재하지 않으므로, 실제로는 게임 오브젝트 컴포넌트로서 등록하기만 하면 됩니다._
