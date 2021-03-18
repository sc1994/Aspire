# Aspire
一个旨在能快速开始一个中后台项目模板, 且在最大程度上考虑个性化. 

#### 功能设计

- [x] 审计仓储
    - 增
    - 软删除
    - 改
    - 查

- [x] 审计实体
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

- [x] Mapper
    - [x] `IAspireMapper.MapTo(...)`
    - [x] `MapperProfileAttribute(...)` 特性指定 `MapTo` 类型(分散的声明映射关系)

- [x] Dynamic Api 依赖了[Panda.DynamicWebApi](https://github.com/pda-team/Panda.DynamicWebApi)
    - [x] `Swagger` 支持
    - [x] `CrudApplication` CRUD的服务抽象类
        - [x] `CurrentRepository`
        - [x] `CreateAsync`
        - [x] `DeleteAsync`
        - [x] `UpdateAsync`
        - [x] `GetAsync`
        - [x] `PagingAsync`
        - [x] `MapToDto` 数据库实体映射到输出
        - [x] `MapToEntity` 输入实体映射到数据库
        - [ ] `FilterPage` 当使用 `PagingAsync` 需事先实现他们的过滤规则
    - 
    - 

- [ ]友好的异常消息
    - [ ] `FriendlyThrowException.ThrowException(...)`