export interface Props {
    handleCrossIconClick : () => void
}
export const CrossLine = (props: Props) => {
    return (
        <div className="crossIconContainer">
            <div className="crossIcon" onClick={props.handleCrossIconClick}>
                <div className="crossline1"></div>
                <div className="crossline2"></div>
            </div>
        </div>
    );
}