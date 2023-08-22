import { IClient } from "../domain/IClient";
import { IUserResource } from "../domain/IUserResource";
import { IEvent } from "../dto/IEvent";

interface Props {
    event: IEvent;
    clientData: IClient[];
    resource : IUserResource[];

}

export const MyEvent = (event: Props) => {
    return (
        <>
            <div>
                <strong>Title: {event.event.title}</strong>
                <div>
                    Client: {event.clientData
                        .filter((client) => client.id == event.event.clientId)
                        .map((client) => `${client.firstName} ${client.lastName}`)}
                </div>
                <div>Description: {event.event.desc}</div>
                <div>Master : {event.resource.filter((worker) => worker.id === event.event.resourceId).map((worker) => `${worker.firstName} ${worker.lastName}`)}</div>
                {event.event.service != null && event.event.service.length > 0 ? (
                    <>
                        <div>
                            Total price: {
                                event.event.service.reduce((totalPrice, service) => totalPrice + parseInt(service.servicePrice), 0)
                            }
                        </div>
                    </>
                ) : (null)

                }

            </div>

        </>
    );
}