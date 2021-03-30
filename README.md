# Aspire
一个旨在能快速开始一个中后台项目模板, 且在最大程度上考虑个性化. 

#### 功能设计

- [x] 审计仓储接口
    - [x] 增
    - [x] 软删除
    - [x] 改
    - [x] 查
    - [x] `IAuditRepositoryOptionsSetup` 继承此接口实现仓储的启动设置(比如注入)

- [x] 审计实体抽象类
    - [x] 主键
	    - [x] 自定义类型
	    - [x] Guid
    - [x] 新增人 Name 
    - [x] 新增人 Account
    - [x] 新增时间
    - [x] 更新人 Name
    - [x] 更新人 Account
    - [x] 更新时间
    - [x] 是否删除
    - [x] 删除人 Name
    - [x] 删除人 Account
    - [x] 删除时间
    - [ ] 租户

- [x] Mapper接口
    - [x] `IAspireMapper.MapTo(...)`
    - [x] `MapperProfileAttribute(...)` 特性指定 `MapTo` 类型(分散的声明映射关系)
    - [x] `IAspireMapperOptionsSetup` 继承此接口实现Mapper的启动设置(比如注入)

- [x] Dynamic Api 依赖了[Panda.DynamicWebApi](https://github.com/pda-team/Panda.DynamicWebApi)
    - [x] `Swagger` 支持
    - [x] `Application` 服务抽象类, 继承此类可将服务暴露成`API`
    - [x] `CrudApplication` CRUD的服务抽象类, 继承此类可实现单表默认的`CRUD` `API`
        - [x] `CurrentRepository`
        - [x] `CreateAsync`
        - [x] `DeleteAsync`
        - [x] `UpdateAsync`
        - [x] `GetAsync`
        - [x] `PagingAsync`
        - [x] `MapToDto` 数据库实体映射到输出
        - [x] `MapToEntity` 输入实体映射到数据库
        - [ ] `FilterPage` 当使用 `PagingAsync` 需事先实现他们的过滤规则

- [ ] 框架服务
    - [ ] 授权服务
        - TODO 目前直接在框架服务中实现 需要独立出去
    - [x] 系统日志服务
        - [x] `SystemLogAppService` 系统日志服务抽象类
            - [x] `FilterAsync` 日志过滤(分页搜索)
            - [x] `GetAsync` 获取日志(单条)
            - [x] `GetSelectItems` 选择项(用于过滤条件的下拉)
            - [x] `DeleteAllSelectItems` 删除全部选择项
            - [x] `GetPageConfig` 页面配置, 支持一定程度的自定义页面
        - [ ] 日志 Dto
            - `ISystemLogFilterInputDto`
            - `ISystemLogFilterOutputDto`
            - `ISystemLogDetailOutputDto`

- [ ] 缓存接口
    - [ ] `IAspireCacheClient` 缓存客户端
    - [ ] `IAspireRedisOptionsSetup` 继承此接口实现缓存的启动设置(比如注入)
    
#### 框架内容 

- [x] 友好的异常消息
    - [x] `FriendlyThrowException.ThrowException(...)`
    - [x] 友好的堆栈 依赖了[Ben.Demystifier](https://github.com/benaadams/Ben.Demystifier)
    - [x] 本地环境中直接可在 `http response` 中查看堆栈


- [x] 统一的响应格式(`http response`)
    - [x] `Code` 在 `ResponseCode` 中定义了一些code的基调, 不过可自定义内容(`int type`).
    - [x] `Messages`
    - [x] `Title`
    - [x] `Result` API响应的结果在这个字段中
    - [x] `StackTrace` 发生异常时的堆栈
    - [x] `StackTraceText` 
    - [x] `WriteToHttpResponseAsync`

- [ ] Jwt 
    - TODO 考虑集成到独立的授权服务中
    
- 其他便利性
    - 静态`DI`服务定位 `ServiceLocator`
        - `ServiceLocator.ServiceProvider.GetService<TypeA>()`
    - 有序`GUID`生成
        - `GuidUtility.NewOrderlyGuid()`
        













#### 功能实现
- TODO 未来这一部分会拆成新的参考


