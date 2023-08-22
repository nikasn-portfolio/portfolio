
interface ImageSelectorProps {
    image_not_found: string,
    images: string[],
    mainImage: string,
    setMainImage: (image: string) => void,
}

const ImageSelector = (props: ImageSelectorProps) => {
    const handleThumbnailClick = (image: string) => {
        props.setMainImage(image);
    };

    return (
        <div>
            <img
                src={props.mainImage}
                onError={(e) => {
                    const target = e.target as HTMLImageElement;
                    target.src = props.image_not_found;
                }}
                alt="Image not found"
                className="itemViewImg"
                id="mainImage"
            />
            {props.images.length > 1 && (
                <div className="scroll-container">
                    {props.images.map((image, index) => (
                        <img
                            className="thumbnail"
                            src={image}
                            alt={`Image ${index + 1}`}
                            key={index}
                            onClick={() => handleThumbnailClick(image)}
                        />
                    ))}
                </div>
            )}
        </div>
    );
};

export default ImageSelector;