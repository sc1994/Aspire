# 框架

将一些 框架基础代码进行打包. 旨在未来的新开项目中直接应用. 做到开箱即用. 以服务及应用的理念进行开发

暂定命名为 `Aspire` 译为 渴望/追求

#### 计划表

| Name                               | Desc                  | Include                                      | Progress |
| ---------------------------------- | --------------------- | -------------------------------------------- | -------- |
| Aspire.AppService                  | 服务                  | 集成服务及应用的特点, 通过此包将服务对外公开 | 0%       |
| Aspire.AppService.Cure             | 增删改查服务          | 自动将单表的Crud通过一定的规律向外开放       | 0%       |
| Aspire.Mapper.Abstract             | Map接口               | 包含基础的对象到对象的映射接口和抽象类       | 0%       |
| Aspire.Mapper.AutoMapper           | AutoMapper            | 使用AutoMapper实现对象到对象的映射接口       | 0%       |
| Aspire.Repository.Abstract         | 仓储接口              | 定义了数据表的基本操作的接口和抽象类         | 0%       |
| Aspire.Repository.FreeSql          | FreeSql               | 使用 FreeSql实现仓储                         | 0%       |
| Aspire.Repository.Audit            | 仓储审计              | 定义了仓储的审计接口                         | 0%       |
| Aspire.Logger.Abstract             | 日志接口              |                                              | 0%       |
| Aspire.Logger.Serilog              | Serilog               |                                              | 0%       |
| Aspire.AppService.ActionLog        | 记录服务的活动日志    |                                              | 0%       |
| Aspire.AppService.FormatResponse   | 格式化响应            |                                              | 0%       |
| Aspire.AppService.FluentValidation | FluentValidation 验证 |                                              | 0%       |
