using System;

namespace scrapper.console
{
    public class Offer
    {
        public Guid OfferId { get; set; }
        public string BannerText { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Location { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Company { get; set; }
        public string OfferDetail { get; set; }
    }
}